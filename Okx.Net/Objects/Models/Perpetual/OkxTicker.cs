using Newtonsoft.Json;

namespace Okx.Net.Objects.Models.Perpetual
{
    /// <summary>
    /// ticker info
    /// </summary>
    public class OkxTicker
    {
        /// <summary>
        /// Symbol
        /// </summary>
        [JsonProperty("instId")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Last traded price
        /// </summary>
        [JsonProperty("last")]
        public decimal LastPrice { get; set; }

    }
}
