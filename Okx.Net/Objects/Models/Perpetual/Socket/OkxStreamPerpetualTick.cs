using CryptoExchange.Net.Converters;
using Newtonsoft.Json;
using System;

namespace Okx.Net.Objects.Models.Perpetual.Socket
{
    /// <summary>
    /// Okx stream tick
    /// </summary>
    public class OkxStreamPerpetualTick
    {
        /// <summary>
        /// Name of symbol
        /// </summary>
        [JsonProperty("instId")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Last price of symbol
        /// </summary>
        [JsonProperty("last")]
        public decimal LastPrice { get; set; }
        /// <summary>
        /// Last size of symbol
        /// </summary>
        [JsonProperty("lastSz")]
        public decimal LastSize { get; set; }
        /// <summary>
        /// Ask price of symbol
        /// </summary>
        [JsonProperty("askPx")]
        public decimal AskPrice { get; set; }
        /// <summary>
        /// Ask size of symbol
        /// </summary>
        [JsonProperty("askSz")]
        public decimal AskSize { get; set; }
        /// <summary>
        /// Bid price of symbol
        /// </summary>
        [JsonProperty("bidPx")]
        public decimal BidPrice { get; set; }
        /// <summary>
        /// Bid size of symbol
        /// </summary>
        [JsonProperty("bidSz")]
        public decimal BidSize { get; set; }
        /// <summary>
        /// Open price in 24h of symbol
        /// </summary>
        [JsonProperty("open24h")]
        public decimal Open { get; set; }
        /// <summary>
        /// High price in 24h of symbol
        /// </summary>
        [JsonProperty("high24h")]
        public decimal High { get; set; }
        /// <summary>
        /// Low price in 24h of symbol
        /// </summary>
        [JsonProperty("low24h")]
        public decimal Low { get; set; }
        /// <summary>
        /// Base Volume
        /// </summary>
        [JsonProperty("volCcy24h")]
        public decimal VolumeCurrency { get; set; }
        /// <summary>
        /// Quote Volume
        /// </summary>
        [JsonProperty("vol24h")]
        public decimal Volume { get; set; }
        /// <summary>
        /// Open price utc0 of symbol
        /// </summary>
        [JsonProperty("sodUtc0")]
        public decimal OpenPriceUtc0 { get; set; }
        /// <summary>
        /// Open price utc8 of symbol
        /// </summary>
        [JsonProperty("sodUtc8")]
        public decimal OpenPriceUtc8 { get; set; }
        /// <summary>
        /// Update time of symbol
        /// </summary>
        [JsonProperty("ts"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime Time { get; set; }
    }
}
