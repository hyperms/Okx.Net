using Newtonsoft.Json;
using Okx.Net.Enums;

namespace Okx.Net.Objects.Models.Perpetual
{
    /// <summary>
    /// Okx set leverage result
    /// </summary>
    public class OkxSetLeverageResult
    {
        /// <summary>
        /// Symbol
        /// </summary>
        [JsonProperty("instId")]
        public string Symbol { get; set; } = string.Empty;

        /// <summary>
        /// Leverage
        /// </summary>
        [JsonProperty("lever")]
        public int Leverage { get; set; }

        /// <summary>
        /// Margin mode
        /// </summary>
        [JsonProperty("mgnMode")]
        public MarginMode MarginMode { get; set; }

        /// <summary>
        /// Position side
        /// </summary>
        [JsonProperty("posSide")]
        public PositionSide PositionSide { get; set; }
    }
}
