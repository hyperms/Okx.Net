using CryptoExchange.Net.Interfaces;
using Okx.Net.Interfaces.Clients.PerpetualApi;

namespace Okx.Net.Interfaces.Clients
{
    /// <summary>
    /// Okx perpetual stream interface
    /// </summary>
    public interface IOkxSocketClient: ISocketClient
    {
        /// <summary>
        /// Perpetual streams
        /// </summary>
        IOkxSocketClientPerpetualStreams PerpetualStreams { get; }
    }
}
