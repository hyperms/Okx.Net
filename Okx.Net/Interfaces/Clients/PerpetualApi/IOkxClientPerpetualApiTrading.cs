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
    /// Okx Perpetual trading endpoints, placing and mananging orders.
    /// </summary>
    public interface IOkxClientPerpetualApiTrading
    {
        /// <summary>
        /// Place a new order
        /// <para><a href="https://www.okx.com/docs-v5/en/#rest-api-trade-place-order" /></para>
        /// </summary>
        /// <param name="symbol">The contract for the order</param>
        /// <param name="side">Side of the order</param>
        /// <param name="positionSide">Position side of the order</param>
        /// <param name="type">Type of order</param>
        /// <param name="marginMode">Margin mode of order</param>
        /// <param name="price">Limit price, only for limit orders</param>
        /// <param name="quantity">Quantity of contract to buy or sell</param>
        /// <param name="reduceOnly">A mark to reduce the position size only. Set to false by default</param>
        /// <param name="clientOrderId">Client order id</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Order details</returns>
        Task<WebCallResult<OkxNewOrder>> PlaceOrderAsync(
            string symbol,
            OrderSide side,
            PositionSide positionSide,
            OrderType type,
            MarginMode marginMode,
            decimal quantity,
            decimal? price = null,
            bool? reduceOnly = null,
            string? clientOrderId = null,
            CancellationToken ct = default);

        /// <summary>
        /// Cancel an order
        /// <para><a href="https://www.okx.com/docs/en/#rest-api-trade-cancel-order" /></para>
        /// </summary>
        /// <param name="symbol">symbol of the order to cancel</param>
        /// <param name="orderId">Id of the order to cancel</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Canceled id</returns>
        Task<WebCallResult<OkxCancelOrder>> CancelOrderAsync(string symbol, long orderId, CancellationToken ct = default);

        /// <summary>
        /// Get list of orders
        /// <para><a href="https://www.okx.com/docs-v5/en/#rest-api-trade-get-order-history-last-3-months" /></para>
        /// </summary>
        /// <param name="symbol">Filter by symbol</param>
        /// <param name="status">Filter by status</param>
        /// <param name="type">Filter by type</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="limit">limit of orders</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of orders</returns>
        Task<WebCallResult<IEnumerable<OkxOrder>>> GetOrdersAsync(string? symbol = null, OrderStatus? status = null, OrderType? type = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Get details on an order
        /// <para><a href="https://www.okx.com/docs/en/#rest-api-trade-get-order-details" /></para>
        /// </summary>
        /// <param name="symbol">symbol of order</param>
        /// <param name="orderId">Id of order to retrieve</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of orders</returns>
        Task<WebCallResult<OkxOrder>> GetOrderAsync(string symbol, long orderId, CancellationToken ct = default);

        /// <summary>
        /// Get details on an order
        /// <para><a href="https://www.okx.com/docs/en/#rest-api-trade-get-order-details" /></para>
        /// </summary>
        /// <param name="symbol">symbol of order</param>
        /// <param name="clientOrderId">Client order id of order to retrieve</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>List of orders</returns>
        Task<WebCallResult<OkxOrder>> GetOrderByClientOrderIdAsync(string symbol, string clientOrderId, CancellationToken ct = default);

    }
}
