using CryptoExchange.Net;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Logging;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Sockets;
using Newtonsoft.Json.Linq;
using Okx.Net.Interfaces.Clients.PerpetualApi;
using Okx.Net.Objects;
using Okx.Net.Objects.Internal;
using Okx.Net.Objects.Models.Perpetual.Socket;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;

namespace Okx.Net.Clients.PerpetualApi
{
    /// <inheritdoc cref="IOkxSocketClientPerpetualStreams" />
    public class OkxSocketClientPerpetualStreams : SocketApiClient, IOkxSocketClientPerpetualStreams
    {
        private readonly OkxSocketClient _baseClient;
        private readonly Log _log;

        internal OkxSocketClientPerpetualStreams(Log log, OkxSocketClient baseClient, OkxSocketClientOptions options)
            : base(options, options.PerpetualStreamsOptions)
        {
            _baseClient = baseClient;
            _log = log;
        }

        /// <inheritdoc />
        protected override AuthenticationProvider CreateAuthenticationProvider(ApiCredentials credentials)
            => new OkxAuthenticationProvider((OkxApiCredentials)credentials);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToTickerUpdatesAsync(string symbol, Action<OkxStreamPerpetualTick> onData, CancellationToken ct = default)
        {
            var innerHandler = new Action<DataEvent<OkxSocketUpdateResponse<IEnumerable<OkxStreamPerpetualTick>>>>(data =>
            {
                foreach (var d in data.Data.Data)
                    onData(d);
            });

            var request = new OkxRequest("subscribe", "tickers", symbol);
            return await _baseClient.SubscribeInternalAsync(this, request, null, false, innerHandler, ct).ConfigureAwait(false);
        }

    }
}
