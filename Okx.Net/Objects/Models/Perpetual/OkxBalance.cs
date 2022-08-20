using CryptoExchange.Net.Converters;
using Newtonsoft.Json;
using System;

namespace Okx.Net.Objects.Models.Perpetual
{
    /// <summary>
    /// balance info
    /// </summary>
    public class OkxBalance
    {
        /// <summary>
        /// Update time of account information
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonProperty("uTime")]
        public DateTime UpdateTime { get; set; }
        /// <summary>
        /// Currency
        /// </summary>
        [JsonProperty("ccy")]
        public string Currency { get; set; } = string.Empty;
        /// <summary>
        /// Available equity of the currency
        /// </summary>
        [JsonProperty("availEq")]
        public decimal Available { get; set; }
        /// <summary>
        /// Frozen balance of the currency
        /// </summary>
        [JsonProperty("frozenBal")]
        public decimal Frozen { get; set; }

    }
}
