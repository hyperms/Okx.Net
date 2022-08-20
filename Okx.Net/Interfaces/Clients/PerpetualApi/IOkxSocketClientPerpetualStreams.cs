using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Sockets;
using Okx.Net.Objects.Models.Perpetual.Socket;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Okx.Net.Interfaces.Clients.PerpetualApi
{
    /// <summary>
    /// Futures streams
    /// </summary>
    public interface IOkxSocketClientPerpetualStreams: IDisposable
    {
        /// <summary>
        /// Subscribe to ticker updates
        /// <para><a href="https://www.okx.com/docs-v5/en/#websocket-api-public-channel-tickers-channel" /></para>
        /// </summary>
        /// <param name="symbol">The symbol to subscribe on</param>
        /// <param name="onData">The data handler</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected and to unsubscribe</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToTickerUpdatesAsync(string symbol, Action<OkxStreamPerpetualTick> onData, CancellationToken ct = default);

    }
}
