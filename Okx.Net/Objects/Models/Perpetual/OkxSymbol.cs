using Newtonsoft.Json;

namespace Okx.Net.Objects.Models.Perpetual
{
    /// <summary>
    /// symbol info
    /// </summary>
    public class OkxSymbol
    {
        /// <summary>
        /// Symbol
        /// </summary>
        [JsonProperty("instId")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Base asset of symbol
        /// </summary>
        [JsonProperty("ctValCcy")]
        public string BaseAsset { get; set; } = string.Empty;
        /// <summary>
        /// Quote asset of symbol
        /// </summary>
        [JsonProperty("settleCcy")]
        public string QuoteAsset { get; set; } = string.Empty;
        /// <summary>
        /// Minimum size of symbol
        /// </summary>
        [JsonProperty("minSz")]
        public decimal MinSize { get; set; }
        /// <summary>
        /// Tick size of symbol
        /// </summary>
        [JsonProperty("tickSz")]
        public decimal TickSize { get; set; }
        /// <summary>
        /// Lot size of symbol
        /// </summary>
        [JsonProperty("lotSz")]
        public decimal LotSize { get; set; }
        /// <summary>
        /// Multiplier of symbol
        /// </summary>
        [JsonProperty("ctMult")]
        public decimal Multiplier { get; set; }
        /// <summary>
        /// Leverage of symbol
        /// </summary>
        [JsonProperty("lever")]
        public int Leverage { get; set; }

    }
}
