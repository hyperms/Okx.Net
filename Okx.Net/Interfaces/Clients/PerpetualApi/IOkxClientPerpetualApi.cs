using System;

namespace Okx.Net.Interfaces.Clients.PerpetualApi
{
    /// <summary>
    /// Okx perpetual API endpoints
    /// </summary>
    public interface IOkxClientPerpetualApi : IDisposable
    {
        /// <summary>
        /// Endpoints related to account settings, info or actions
        /// </summary>
        public IOkxClientPerpetualApiAccount Account { get; }

        /// <summary>
        /// Endpoints related to retrieving market data
        /// </summary>
        public IOkxClientPerpetualApiExchangeData ExchangeData { get; }

        /// <summary>
        /// Endpoints related to orders and trades
        /// </summary>
        public IOkxClientPerpetualApiTrading Trading { get; }
    }
}
