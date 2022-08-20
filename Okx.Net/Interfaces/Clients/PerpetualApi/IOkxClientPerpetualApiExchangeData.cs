using CryptoExchange.Net.Objects;
using Okx.Net.Enums;
using Okx.Net.Objects.Models.Perpetual;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Okx.Net.Interfaces.Clients.PerpetualApi
{
    /// <summary>
    /// Okx perpetual exchange data endpoints. Exchange data includes market data (tickers, order books, etc) and system status.
    /// </summary>
    public interface IOkxClientPerpetualApiExchangeData
    {
        /// <summary>
        /// Get symbol list
        /// <para><a href="https://www.okx.com/docs/en/#rest-api-market-data-get-tickers" /></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<OkxSymbol>>> GetSymbolsAsync(CancellationToken ct = default);

        /// <summary>
        /// Get a symbol
        /// <para><a href="https://www.okx.com/docs/en/#rest-api-market-data-get-ticker" /></para>
        /// </summary>
        /// <param name="symbol">Symbol of the contract</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<OkxSymbol>> GetSymbolAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Get the server time
        /// <para><a href="https://www.okx.com/docs/en/#rest-api-public-data-get-system-time" /></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<DateTime>> GetServerTimeAsync(CancellationToken ct = default);

        /// <summary>
        /// Get kline data
        /// <para><a href="https://www.okx.com/docs/en/#rest-api-market-data-get-candlesticks" /></para>
        /// </summary>
        /// <param name="symbol">Symbol</param>
        /// <param name="interval">Interval of the klines</param>
        /// <param name="startTime">Start time to retrieve klines from</param>
        /// <param name="endTime">End time to retrieve klines for</param>
        /// <param name="limit">Number of results per request</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<OkxKline>>> GetKlinesAsync(string symbol, KlineInterval interval, DateTime? startTime = null, DateTime? endTime = null, int limit = 100, CancellationToken ct = default);

    }
}
