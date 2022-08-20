using CryptoExchange.Net;
using CryptoExchange.Net.Converters;
using CryptoExchange.Net.Objects;
using Newtonsoft.Json;
using Okx.Net.Converters;
using Okx.Net.Enums;
using Okx.Net.Interfaces.Clients.PerpetualApi;
using Okx.Net.Objects.Models.Perpetual;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Okx.Net.Clients.PerpetualApi
{
    /// <inheritdoc />
    public class OkxClientPerpetualApiTrading : IOkxClientPerpetualApiTrading
    {
        private readonly OkxClientPerpetualApi _baseClient;

        internal OkxClientPerpetualApiTrading(OkxClientPerpetualApi baseClient)
        {
            _baseClient = baseClient;
        }

        #region Orders

        /// <inheritdoc />
        public async Task<WebCallResult<OkxNewOrder>> PlaceOrderAsync(
            string symbol,
            OrderSide side,
            PositionSide positionSide,
            OrderType type,
            MarginMode marginMode,
            decimal quantity,
            decimal? price = null,
            bool? reduceOnly = null,
            string? clientOrderId = null,
            CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddParameter("instId", symbol);
            parameters.AddParameter("side", JsonConvert.SerializeObject(side, new OrderSideConverter(false)));
            parameters.AddParameter("ordType", JsonConvert.SerializeObject(type, new OrderTypeConverter(false)));
            parameters.AddParameter("tdMode", JsonConvert.SerializeObject(marginMode, new MarginModeConverter(false)));
            parameters.AddParameter("sz", quantity.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("clOrdId", clientOrderId);
            parameters.AddOptionalParameter("posSide", JsonConvert.SerializeObject(positionSide, new PositionSideConverter(false)));
            parameters.AddOptionalParameter("reduceOnly", reduceOnly?.ToString());
            parameters.AddOptionalParameter("px", price?.ToString(CultureInfo.InvariantCulture));

            return await _baseClient.Execute<OkxNewOrder>(_baseClient.GetUri("trade/order"), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<OkxCancelOrder>> CancelOrderAsync(string symbol, long orderId, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddParameter("instId", symbol);
            parameters.AddParameter("ordId", orderId.ToString());

            return await _baseClient.Execute<OkxCancelOrder>(_baseClient.GetUri("trade/cancel-order"), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<OkxOrder>>> GetOrdersAsync(string? symbol = null, OrderStatus? status = null, OrderType? type = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddParameter("instType", "SWAP");
            parameters.AddOptionalParameter("instId", symbol);
            parameters.AddOptionalParameter("state", status == null ? null : JsonConvert.SerializeObject(status, new OrderStatusConverter(false)));
            parameters.AddOptionalParameter("ordType", type == null ? null : JsonConvert.SerializeObject(type, new OrderTypeConverter(false)));
            parameters.AddOptionalParameter("before", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("after", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("limit", limit);
            return await _baseClient.Execute<IEnumerable<OkxOrder>>(_baseClient.GetUri("trade/orders-history-archive"), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<OkxOrder>> GetOrderAsync(string symbol, long orderId, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddParameter("instId", symbol);
            parameters.AddParameter("ordId", orderId.ToString());
            return await _baseClient.Execute<OkxOrder>(_baseClient.GetUri("trade/order"), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<OkxOrder>> GetOrderByClientOrderIdAsync(string symbol, string clientOrderId, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddParameter("instId", symbol);
            parameters.AddParameter("clOrdId", clientOrderId);
            return await _baseClient.Execute<OkxOrder>(_baseClient.GetUri("trade/order"), HttpMethod.Get, ct, parameters, true).ConfigureAwait(false);
        }
        #endregion
    }
}
