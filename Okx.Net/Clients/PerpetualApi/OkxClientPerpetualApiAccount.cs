using CryptoExchange.Net;
using CryptoExchange.Net.Objects;
using Newtonsoft.Json;
using Okx.Net.Converters;
using Okx.Net.Enums;
using Okx.Net.Interfaces.Clients.PerpetualApi;
using Okx.Net.Objects.Internal;
using Okx.Net.Objects.Models.Perpetual;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Okx.Net.Clients.PerpetualApi
{
    /// <inheritdoc />
    internal class OkxClientPerpetualApiAccount : IOkxClientPerpetualApiAccount
    {
        private readonly OkxClientPerpetualApi _baseClient;

        internal OkxClientPerpetualApiAccount(OkxClientPerpetualApi baseClient)
        {
            _baseClient = baseClient;
        }

        #region Balance

        /// <inheritdoc />
        public async Task<WebCallResult<OkxBalanceResult>> GetBalanceAsync(string? currency = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("ccy", currency);
            return await _baseClient.Execute<OkxBalanceResult>(_baseClient.GetUri("account/balance"), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }
        #endregion

        #region Positions

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<OkxPosition>>> GetPositionsAsync(CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("instType", "SWAP");
            return await _baseClient.Execute<IEnumerable<OkxPosition>>(_baseClient.GetUri("account/positions"), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }

        #endregion

        #region Leverage

        /// <inheritdoc />
        public async Task<WebCallResult<OkxSetLeverageResult>> SetLeverageAsync(string symbol, int leverage, MarginMode marginMode, PositionSide positionSide, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddParameter("instId", symbol);
            parameters.AddParameter("lever", leverage.ToString());
            parameters.AddParameter("mgnMode", JsonConvert.SerializeObject(marginMode, new MarginModeConverter(false)));
            parameters.AddOptionalParameter("posSide", JsonConvert.SerializeObject(positionSide, new PositionSideConverter(false)));
            return await _baseClient.Execute<OkxSetLeverageResult>(_baseClient.GetUri("account/set-leverage"), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

        #endregion

        #region Websocket token

        internal async Task<WebCallResult<OkxToken>> GetWebsocketToken(bool authenticated, CancellationToken ct = default)
        {
            return await _baseClient.Execute<OkxToken>(_baseClient.GetUri(authenticated ? "bullet-private" : "bullet-public"), method: HttpMethod.Post, ct, signed: authenticated).ConfigureAwait(false);
        }

        #endregion
    }
}
