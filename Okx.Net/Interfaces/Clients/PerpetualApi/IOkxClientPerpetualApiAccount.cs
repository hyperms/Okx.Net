using CryptoExchange.Net.Objects;
using Okx.Net.Enums;
using Okx.Net.Objects.Models.Perpetual;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Okx.Net.Interfaces.Clients.PerpetualApi
{
    /// <summary>
    /// Okx Perprtual account endpoints. Account endpoints include balance info, position info
    /// </summary>
    public interface IOkxClientPerpetualApiAccount
    {
        /// <summary>
        /// Gets account balances
        /// <para><a href="https://okx.com/docs/en/#rest-api-account-get-balance" /></para>
        /// </summary>
        /// <param name="currency">Single currency or multiple currencies (no more than 20) separated with comma, e.g. BTC or BTC,ETH</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of balances</returns>
        Task<WebCallResult<OkxBalanceResult>> GetBalanceAsync(string? currency = null, CancellationToken ct = default);

        /// <summary>
        /// Retrieve information on your positions
        /// <para><a href="https://okx.com/docs/en/#rest-api-account-get-positions" /></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Position info</returns>
        Task<WebCallResult<IEnumerable<OkxPosition>>> GetPositionsAsync(CancellationToken ct = default);

        /// <summary>
        /// Set symbol levereage
        /// <para><a href="https://www.okx.com/docs/en/#rest-api-account-set-leverage" /></para>
        /// </summary>
        /// <param name="symbol">Symbol</param>
        /// <param name="leverage">Leverage</param>
        /// <param name="marginMode">Margin mode</param>
        /// <param name="positionSide">Position side</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Position info</returns>

        Task<WebCallResult<OkxSetLeverageResult>> SetLeverageAsync(string symbol, int leverage, MarginMode marginMode, PositionSide positionSide, CancellationToken ct = default);
    }
}
