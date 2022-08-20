using CryptoExchange.Net;
using CryptoExchange.Net.Objects;
using Newtonsoft.Json.Linq;
using Okx.Net.Clients.PerpetualApi;
using Okx.Net.Interfaces.Clients;
using Okx.Net.Interfaces.Clients.PerpetualApi;
using Okx.Net.Objects;
using Okx.Net.Objects.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Okx.Net.Clients
{
    /// <inheritdoc cref="IOkxClient" />
    public class OkxClient : BaseRestClient, IOkxClient
    {
        /// <inheritdoc />
        public IOkxClientPerpetualApi PerpetualApi { get; }

        /// <summary>
        /// Create a new instance of KucoinClient using the default options
        /// </summary>
        public OkxClient() : this(OkxClientOptions.Default)
        {

        }

        /// <summary>
        /// Create a new instance of KucoinClient using provided options
        /// </summary>
        /// <param name="options">The options to use for this client</param>
        public OkxClient(OkxClientOptions options) : base("Okx", options)
        {
            PerpetualApi = AddApiClient(new OkxClientPerpetualApi(log, this, options));
        }

        /// <inheritdoc />
        public void SetApiCredentials(OkxApiCredentials credentials)
        {
            ((OkxClientPerpetualApi)PerpetualApi).SetApiCredentials(credentials);
        }

        /// <summary>
        /// Set the default options to be used when creating new clients
        /// </summary>
        /// <param name="options">Options to use as default</param>
        public static void SetDefaultOptions(OkxClientOptions options)
        {
            OkxClientOptions.Default = options;
        }

        /// <inheritdoc />
        protected override Error ParseErrorResponse(JToken error)
        {
            if (!error.HasValues)
            {
                var errorBody = error.ToString();
                return new ServerError(string.IsNullOrEmpty(errorBody) ? "Unknown error" : errorBody);
            }

            if (error["code"] != null && error["msg"] != null)
            {
                var result = error.ToObject<OkxResult<object>>();
                if (result == null)
                    return new ServerError(error["msg"]!.ToString());

                return new ServerError(result.Code, result.Message!);
            }

            return new ServerError(error.ToString());
        }

        internal async Task<WebCallResult> Execute(RestApiClient apiClient, Uri uri, HttpMethod method, CancellationToken ct, Dictionary<string, object>? parameters = null, bool signed = false, HttpMethodParameterPosition? parameterPosition = null)
        {
            var result = await SendRequestAsync<OkxResult<object>>(apiClient, uri, method, ct, parameters, signed, parameterPosition).ConfigureAwait(false);
            if (!result)
                return result.AsDatalessError(result.Error!);

            if (result.Data.Code != 0)
                return result.AsDatalessError(new ServerError(result.Data.Code, result.Data.Message ?? "-"));

            return result.AsDataless();
        }

        internal async Task<WebCallResult<T>> Execute<T>(RestApiClient apiClient, Uri uri, HttpMethod method, CancellationToken ct, Dictionary<string, object>? parameters = null, bool signed = false, int weight = 1, bool ignoreRatelimit = false, HttpMethodParameterPosition? parameterPosition = null)
        {
            var result = await SendRequestAsync<OkxResult<IEnumerable<T>>>(apiClient, uri, method, ct, parameters, signed, parameterPosition, requestWeight: weight, ignoreRatelimit: ignoreRatelimit).ConfigureAwait(false);
            if (!result)
                return result.AsError<T>(result.Error!);

            if (result.Data.Code != 0)
                return result.AsError<T>(new ServerError(result.Data.Code, result.Data.Message ?? "-"));

            return result.As(result.Data.Data.FirstOrDefault());
        }

        internal async Task<WebCallResult<T>> ExecuteList<T>(RestApiClient apiClient, Uri uri, HttpMethod method, CancellationToken ct, Dictionary<string, object>? parameters = null, bool signed = false, int weight = 1, bool ignoreRatelimit = false, HttpMethodParameterPosition? parameterPosition = null)
        {
            var result = await SendRequestAsync<OkxResult<T>>(apiClient, uri, method, ct, parameters, signed, parameterPosition, requestWeight: weight, ignoreRatelimit: ignoreRatelimit).ConfigureAwait(false);
            if (!result)
                return result.AsError<T>(result.Error!);

            if (result.Data.Code != 0)
                return result.AsError<T>(new ServerError(result.Data.Code, result.Data.Message ?? "-"));

            return result.As(result.Data.Data);
        }
    }
}
