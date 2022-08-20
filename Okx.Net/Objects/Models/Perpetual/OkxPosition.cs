using CryptoExchange.Net.Converters;
using Newtonsoft.Json;
using Okx.Net.Enums;
using System;

namespace Okx.Net.Objects.Models.Perpetual
{
    /// <summary>
    /// position info
    /// </summary>
    public class OkxPosition
    {
        /// <summary>
        /// Position ID
        /// </summary>
        [JsonProperty("posId")]
        public string Id { get; set; } = string.Empty;
        /// <summary>
        /// Margin mode: cross, isolated
        /// </summary>
        [JsonProperty("mgnMode")]
        public string MarginMode { get; set; } = string.Empty;
        /// <summary>
        /// Instrument type
        /// </summary>
        [JsonProperty("instType")]
        public string InstrumentType { get; set; } = string.Empty;
        /// <summary>
        /// Position side: long, short, net 
        /// </summary>
        [JsonProperty("posSide")]
        public PositionSide PositionSide { get; set; }
        /// <summary>
        /// Quantity of positions. In the mode of autonomous transfer from position to position, after the deposit is transferred, a position with pos of 0 will be generated
        /// </summary>
        [JsonProperty("pos")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// Position that can be closed
        /// Only applicable to MARGIN, FUTURES/SWAP in the long-short mode, OPTION in Simple and isolated OPTION in margin Account.
        /// </summary>
        [JsonProperty("availPos")]
        public decimal AvailableQuantity { get; set; }
        /// <summary>
        /// Symbol
        /// </summary>
        [JsonProperty("posCcy")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Average open price
        /// </summary>
        [JsonProperty("avgPx")]
        public decimal AverageOpenPrice { get; set; }
        /// <summary>
        /// Mark price
        /// </summary>
        [JsonProperty("markPx")]
        public decimal MarkPrice { get; set; }
        /// <summary>
        /// Unrealized profit and loss
        /// </summary>
        [JsonProperty("upl")]
        public decimal UnrealizedPnl { get; set; }
        /// <summary>
        /// Unrealized profit and loss ratio
        /// </summary>
        [JsonProperty("uplRatio")]
        public decimal UnrealizedPnlRatio { get; set; }
        /// <summary>
        /// Instrument ID, e.g. BTC-USD-180216
        /// </summary>
        [JsonProperty("instId")]
        public string InstrumentId { get; set; } = string.Empty;
        /// <summary>
        /// Leverage, not applicable to OPTION
        /// </summary>
        [JsonProperty("lever")]
        public int Leverage { get; set; }
        /// <summary>
        /// Estimated liquidation price
        /// Not applicable to OPTION
        /// </summary>
        [JsonProperty("liqPx")]
        public decimal LiquidationPrice { get; set; }
        /// <summary>
        /// Creation time
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonProperty("cTime")]
        public DateTime CreationTime { get; set; }
        /// <summary>
        /// Latest time position was adjusted
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonProperty("uTime")]
        public DateTime UpdateTime { get; set; }
    }
}
