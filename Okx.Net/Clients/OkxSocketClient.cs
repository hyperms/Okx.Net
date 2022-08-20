using CryptoExchange.Net;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Sockets;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Okx.Net.Clients.PerpetualApi;
using Okx.Net.Interfaces.Clients;
using Okx.Net.Interfaces.Clients.PerpetualApi;
using Okx.Net.Objects;
using Okx.Net.Objects.Internal;
using System;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Okx.Net.Clients
{
    /// <inheritdoc cref="IOkxSocketClient" />
    public class OkxSocketClient : BaseSocketClient, IOkxSocketClient
    {
        #region Api clients

        /// <inheritdoc />
        public IOkxSocketClientPerpetualStreams PerpetualStreams { get; }

        #endregion

        /// <summary>
        /// Create a new instance of KucoinSocketClient using the default options
        /// </summary>
        public OkxSocketClient() : this(OkxSocketClientOptions.Default)
        {
        }

        /// <summary>
        /// Create a new instance of KucoinSocketClient using provided options
        /// </summary>
        /// <param name="options">The options to use for this client</param>
        public OkxSocketClient(OkxSocketClientOptions options) : base("Okx", options)
        {
            SendPeriodic("Ping", TimeSpan.FromSeconds(30), (connection) => new OkxPing()
            {
                Id = Math.Round((DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalMilliseconds).ToString(CultureInfo.InvariantCulture),
                Type = "ping"
            });

            AddGenericHandler("Ping", (messageEvent) => { });
            AddGenericHandler("Welcome", (messageEvent) => { });

            PerpetualStreams = AddApiClient(new OkxSocketClientPerpetualStreams(log, this, options));
        }

        /// <summary>
        /// Set the default options to be used when creating new clients
        /// </summary>
        /// <param name="options">Options to use as default</param>
        public static void SetDefaultOptions(OkxSocketClientOptions options)
        {
            OkxSocketClientOptions.Default = options;
        }

        /// <summary>
        /// Set the API credentials to use in this client
        /// </summary>
        /// <param name="credentials">Credentials to use</param>
        public void SetApiCredentials(OkxApiCredentials credentials)
        {
            ((OkxSocketClientPerpetualStreams)PerpetualStreams).SetApiCredentials(credentials);
        }

        internal Task<CallResult<UpdateSubscription>> SubscribeInternalAsync<T>(SocketApiClient apiClient, string url, object? request, string? identifier, bool authenticated, Action<DataEvent<T>> dataHandler, CancellationToken ct)
            => SubscribeAsync(apiClient, url, request, identifier, authenticated, dataHandler, ct);

        internal Task<CallResult<UpdateSubscription>> SubscribeInternalAsync<T>(SocketApiClient apiClient, object? request, string? identifier, bool authenticated, Action<DataEvent<T>> dataHandler, CancellationToken ct)
            => SubscribeAsync(apiClient, request, identifier, authenticated, dataHandler, ct);

        /// <inheritdoc />
        protected override async Task<CallResult<string?>> GetConnectionUrlAsync(SocketApiClient apiClient, string address, bool authenticated)
        {
            address = authenticated
                ? "wss://ws.okx.com:8443/ws/v5/private"
                : "wss://ws.okx.com:8443/ws/v5/public";

            return new CallResult<string?>(address);
        }

        /// <inheritdoc />
        public override async Task<Uri?> GetReconnectUriAsync(SocketApiClient apiClient, SocketConnection connection)
        {
            var result = await GetConnectionUrlAsync(apiClient, connection.ConnectionUri.ToString(), connection.Subscriptions.Any(s => s.Authenticated)).ConfigureAwait(false);
            if (!result)
                return null;

            return new Uri(result.Data);
        }

        /// <inheritdoc />
        protected override bool HandleQueryResponse<T>(SocketConnection s, object request, JToken data, out CallResult<T> callResult)
        {
            callResult = null;

            // Ping Request
            if (request.ToString() == "ping" && data.ToString() == "pong")
            {
                return true;
            }

            // Check for Error
            if (data is JObject && data["event"] != null && (string)data["event"]! == "error" && data["code"] != null && data["msg"] != null)
            {
                log.Write(LogLevel.Warning, "Query failed: " + (string)data["msg"]!);
                callResult = new CallResult<T>(new ServerError($"{(string)data["code"]!}, {(string)data["msg"]!}"));
                return true;
            }

            // Login Request
            if (data is JObject && data["event"] != null && (string)data["event"]! == "login")
            {
                var desResult = Deserialize<T>(data);
                if (!desResult)
                {
                    log.Write(LogLevel.Warning, $"Failed to deserialize data: {desResult.Error}. Data: {data}");
                    return false;
                }

                callResult = new CallResult<T>(desResult.Data);
                return true;
            }

            return false;
        }

        /// <inheritdoc />
        protected override bool HandleSubscriptionResponse(SocketConnection s, SocketSubscription subscription, object request, JToken message, out CallResult<object>? callResult)
        {
            return OkxHandleSubscriptionResponse(s, subscription, request, message, out callResult);
        }

        protected virtual bool OkxHandleSubscriptionResponse(SocketConnection s, SocketSubscription subscription, object request, JToken message, out CallResult<object> callResult)
        {
            callResult = null;

            // Check for Error
            // 30040: {0} Channel : {1} doesn't exist
            if (message["event"] != null && (string)message["event"]! == "error" && message["errorCode"] != null && (string)message["errorCode"]! == "30040")
            {
                log.Write(LogLevel.Warning, "Subscription failed: " + (string)message["message"]!);
                callResult = new CallResult<object>(new ServerError($"{(string)message["errorCode"]!}, {(string)message["message"]!}"));
                return true;
            }

            // Check for Success
            if (message["event"] != null && (string)message["event"]! == "subscribe" && message["arg"]["channel"] != null)
            {
                if (request is OkxRequest socRequest)
                {
                    if (socRequest.Arguments.FirstOrDefault().Channel == (string)message["arg"]["channel"]!)
                    {
                        log.Write(LogLevel.Debug, "Subscription completed");
                        callResult = new CallResult<object>(true);
                        return true;
                    }
                }
            }

            return false;
        }

        /// <inheritdoc />
        protected override bool MessageMatchesHandler(SocketConnection socketConnection, JToken message, object request)
        {
            // Ping Request
            if (request.ToString() == "ping" && message.ToString() == "pong")
                return true;

            // Check Point
            if (message.Type != JTokenType.Object)
                return false;

            // Socket Request
            if (request is OkxRequest hRequest)
            {
                // Check for Error
                if (message is JObject && message["event"] != null && (string)message["event"]! == "error" && message["code"] != null && message["msg"] != null)
                    return false;

                // Check for Channel
                if (hRequest.Operation != "subscribe" || message["arg"]["channel"] == null)
                    return false;

                // Compare Request and Response Arguments
                var reqArg = hRequest.Arguments.FirstOrDefault();
                var resArg = JsonConvert.DeserializeObject<OkxRequestArgument>(message["arg"].ToString());

                // Check Data
                var data = message["data"];
                if (data?.HasValues ?? false)
                {
                    if (reqArg.Channel == resArg.Channel &&
                        reqArg.Underlying == resArg.Underlying &&
                        reqArg.InstrumentId == resArg.InstrumentId)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        /// <inheritdoc />
        protected override bool MessageMatchesHandler(SocketConnection socketConnection, JToken message, string identifier)
        {
            return true;
        }

        /// <inheritdoc />
        protected override Task<CallResult<bool>> AuthenticateSocketAsync(SocketConnection s)
        {
            return Task.FromResult(new CallResult<bool>(true));
        }

        /// <inheritdoc />
        protected override async Task<bool> UnsubscribeAsync(SocketConnection connection, SocketSubscription s)
        {
            return await OkxUnsubscribe(connection, s).ConfigureAwait(false);
        }

        protected virtual async Task<bool> OkxUnsubscribe(SocketConnection connection, SocketSubscription s)
        {
            if (s == null || s.Request == null)
                return false;

            var request = new OkxRequest("unsubscribe", ((OkxRequest)s.Request).Arguments);
            await connection.SendAndWaitAsync(request, TimeSpan.FromSeconds(10), data =>
            {
                if (data.Type != JTokenType.Object)
                    return false;

                if ((string)data["event"] == "unsubscribe")
                {
                    return (string)data["arg"]["channel"] == request.Arguments.FirstOrDefault().Channel;
                }

                return false;
            }).ConfigureAwait(false);
            return false;
        }


        internal static void InvokeHandler<T>(T data, Action<T> handler)
        {
            if (Equals(data, default(T)!))
                return;

            handler?.Invoke(data!);
        }

        internal T GetData<T>(DataEvent<JToken> tokenData)
        {
            var desResult = Deserialize<OkxUpdateMessage<T>>(tokenData.Data);
            if (!desResult)
            {
                log.Write(LogLevel.Warning, "Failed to deserialize update: " + desResult.Error + ", data: " + tokenData);
                return default!;
            }
            return desResult.Data.Data;
        }

        internal CallResult<T> DeserializeInternal<T>(JToken data, JsonSerializer? serializer = null, int? requestId = null)
            => Deserialize<T>(data, serializer, requestId);

        internal int NextIdInternal() => NextId();
    }
}
