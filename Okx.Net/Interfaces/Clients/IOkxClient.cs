using CryptoExchange.Net.Interfaces;
using Okx.Net.Interfaces.Clients.PerpetualApi;

namespace Okx.Net.Interfaces.Clients
{
    /// <summary>
    /// Client for accessing the Okx Rest API. 
    /// </summary>
    public interface IOkxClient: IRestClient
    {
        /// <summary>
        /// Perpetual API endpoints
        /// </summary>
        IOkxClientPerpetualApi PerpetualApi { get; }
    }
}
