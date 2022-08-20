using CryptoExchange.Net.Converters;
using Newtonsoft.Json;
using Okx.Net.Enums;
using System;

namespace Okx.Net.Objects.Models.Perpetual
{
    /// <summary>
    /// Okx base order
    /// </summary>
    public class OkxBaseOrder
    {
        /// <summary>
        /// The id of the order
        /// </summary>
        [JsonProperty("ordId")]
        public long Id { get; set; }
        /// <summary>
        /// The client order id of the order
        /// </summary>
        [JsonProperty("clOrdId")]
        public string ClientOrderId { get; set; } = string.Empty;

    }

    /// <summary>
    /// Okx new order
    /// </summary>
    public class OkxNewOrder : OkxBaseOrder
    {
        /// <summary>
        /// The code of the request
        /// </summary>
        [JsonProperty("sCode")]
        public int Code { get; set; }
    }

    /// <summary>
    /// Okx cancel order
    /// </summary>
    public class OkxCancelOrder : OkxBaseOrder
    {
        /// <summary>
        /// The code of the request
        /// </summary>
        [JsonProperty("sCode")]
        public int Code { get; set; }
    }

    /// <summary>
    /// Okx order
    /// </summary>
    public class OkxOrder : OkxBaseOrder
    {
        /// <summary>
        /// Instrument type
        /// </summary>
        [JsonProperty("instType")]
        public string InstrumentType { get; set; } = string.Empty;
        /// <summary>
        /// Symbol
        /// </summary>
        [JsonProperty("instId")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Price
        /// </summary>
        [JsonProperty("px")]
        public decimal Price { get; set; }
        /// <summary>
        /// Average filled price
        /// </summary>
        [JsonProperty("avgPx")]
        public decimal? AveragePrice { get; set; }
        /// <summary>
        /// Quantity
        /// </summary>
        [JsonProperty("sz")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// Accumulated fill quantity
        /// </summary>
        [JsonProperty("accFillSz")]
        public decimal FilledQuantity { get; set; }
        /// <summary>
        /// Order side
        /// </summary>
        [JsonProperty("side")]
        public OrderSide Side { get; set; }
        /// <summary>
        /// Order type
        /// </summary>
        [JsonProperty("ordType")]
        public OrderType Type { get; set; }
        /// <summary>
        /// Leverage
        /// </summary>
        [JsonProperty("lever")]
        public int Leverage { get; set; }
        /// <summary>
        /// fee
        /// </summary>
        [JsonProperty("fee")]
        public decimal Fee { get; set; }
        /// <summary>
        /// The status of the order
        /// </summary>
        [JsonProperty("state")]
        public OrderStatus Status { get; set; }
        /// <summary>
        /// Update time
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonProperty("uTime")]
        public DateTime UpdateTime { get; set; }
        /// <summary>
        /// Creation time
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonProperty("cTime")]
        public DateTime CreateTime { get; set; }
    }
}
