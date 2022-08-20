using CryptoExchange.Net;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.CommonObjects;
using CryptoExchange.Net.Logging;
using CryptoExchange.Net.Objects;
using Okx.Net.Interfaces.Clients.PerpetualApi;
using Okx.Net.Objects;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Okx.Net.Clients.PerpetualApi
{
    /// <inheritdoc cref="IOkxClientPerpetualApi" />
    public class OkxClientPerpetualApi : RestApiClient, IOkxClientPerpetualApi
    {
        private readonly OkxClient _baseClient;
        private readonly OkxClientOptions _options;
        private readonly Log _log;

        internal static TimeSyncState TimeSyncState = new TimeSyncState("Perpetual Api");

        /// <summary>
        /// Event triggered when an order is placed via this client. Only available for Spot orders
        /// </summary>
        public event Action<OrderId>? OnOrderPlaced;
        /// <summary>
        /// Event triggered when an order is canceled via this client. Note that this does not trigger when using CancelAllOrdersAsync. Only available for Spot orders
        /// </summary>
        public event Action<OrderId>? OnOrderCanceled;

        /// <inheritdoc />
        public string ExchangeName => "Okx";

        /// <inheritdoc />
        public IOkxClientPerpetualApiAccount Account { get; }

        /// <inheritdoc />
        public IOkxClientPerpetualApiExchangeData ExchangeData { get; }

        /// <inheritdoc />
        public IOkxClientPerpetualApiTrading Trading { get; }

        internal OkxClientPerpetualApi(Log log, OkxClient baseClient, OkxClientOptions options)
            : base(options, options.PerpetualApiOptions)
        {
            _baseClient = baseClient;
            _options = options;
            _log = log;

            Account = new OkxClientPerpetualApiAccount(this);
            ExchangeData = new OkxClientPerpetualApiExchangeData(this);
            Trading = new OkxClientPerpetualApiTrading(this);

            ParameterPositions[HttpMethod.Delete] = HttpMethodParameterPosition.InUri;
        }

        /// <inheritdoc />
        protected override AuthenticationProvider CreateAuthenticationProvider(ApiCredentials credentials)
            => new OkxAuthenticationProvider((OkxApiCredentials)credentials);

        internal Task<WebCallResult> Execute(Uri uri, HttpMethod method, CancellationToken ct, Dictionary<string, object>? parameters = null, bool signed = false)
         => _baseClient.Execute(this, uri, method, ct, parameters, signed);

        internal Task<WebCallResult<T>> Execute<T>(Uri uri, HttpMethod method, CancellationToken ct, Dictionary<string, object>? parameters = null, bool signed = false, int weight = 1, bool ignoreRatelimit = false)
         => _baseClient.Execute<T>(this, uri, method, ct, parameters, signed, weight, ignoreRatelimit: ignoreRatelimit);

        internal Task<WebCallResult<T>> ExecuteList<T>(Uri uri, HttpMethod method, CancellationToken ct, Dictionary<string, object>? parameters = null, bool signed = false, int weight = 1, bool ignoreRatelimit = false)
         => _baseClient.ExecuteList<T>(this, uri, method, ct, parameters, signed, weight, ignoreRatelimit: ignoreRatelimit);

        internal Uri GetUri(string path, int apiVersion = 5)
        {
            return new Uri(BaseAddress.AppendPath("v" + apiVersion, path));
        }

        /// <inheritdoc />
        protected override Task<WebCallResult<DateTime>> GetServerTimestampAsync()
            => ExchangeData.GetServerTimeAsync();

        /// <inheritdoc />
        public override TimeSyncInfo GetTimeSyncInfo()
            => new TimeSyncInfo(_log, _options.PerpetualApiOptions.AutoTimestamp, _options.PerpetualApiOptions.TimestampRecalculationInterval, TimeSyncState);

        /// <inheritdoc />
        public override TimeSpan GetTimeOffset()
            => TimeSyncState.TimeOffset;

        internal void InvokeOrderPlaced(OrderId id)
        {
            OnOrderPlaced?.Invoke(id);
        }

        internal void InvokeOrderCanceled(OrderId id)
        {
            OnOrderCanceled?.Invoke(id);
        }
    }
}
