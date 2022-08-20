using CryptoExchange.Net;
using CryptoExchange.Net.Converters;
using CryptoExchange.Net.Objects;
using Newtonsoft.Json;
using Okx.Net.Converters;
using Okx.Net.Enums;
using Okx.Net.Interfaces.Clients.PerpetualApi;
using Okx.Net.Objects.Models;
using Okx.Net.Objects.Models.Perpetual;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Okx.Net.Clients.PerpetualApi
{
    /// <inheritdoc />
    public class OkxClientPerpetualApiExchangeData : IOkxClientPerpetualApiExchangeData
    {
        private readonly OkxClientPerpetualApi _baseClient;

        internal OkxClientPerpetualApiExchangeData(OkxClientPerpetualApi baseClient)
        {
            _baseClient = baseClient;
        }

        #region Symbol

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<OkxSymbol>>> GetSymbolsAsync(CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddParameter("instType", "SWAP");
            return await _baseClient.ExecuteList<IEnumerable<OkxSymbol>>(_baseClient.GetUri("public/instruments"), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<OkxSymbol>> GetSymbolAsync(string symbol, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddParameter("instType", "SWAP");
            parameters.AddParameter("instId", symbol);
            return await _baseClient.Execute<OkxSymbol>(_baseClient.GetUri("public/instruments"), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        }

        #endregion

        #region Time

        /// <inheritdoc />
        public async Task<WebCallResult<DateTime>> GetServerTimeAsync(CancellationToken ct = default)
        {
            var result = await _baseClient.Execute<OkxSystemTime>(_baseClient.GetUri("public/time"), HttpMethod.Get, ct, ignoreRatelimit: true).ConfigureAwait(false);
            return result.As(result ? result.Data.SystemTime : default);
        }

        #endregion

        #region Klines

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<OkxKline>>> GetKlinesAsync(string symbol, KlineInterval interval, DateTime? startTime = null, DateTime? endTime = null, int limit = 100, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddParameter("instId", symbol);
            parameters.AddParameter("bar", JsonConvert.SerializeObject(interval, new KlineIntervalConverter(false)));
            parameters.AddOptionalParameter("after", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("before", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("limit", limit.ToString());
            return await _baseClient.ExecuteList<IEnumerable<OkxKline>>(_baseClient.GetUri("market/candles"), HttpMethod.Get, ct, parameters).ConfigureAwait(false);
        }

        #endregion
    }
}
