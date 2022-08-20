using CryptoExchange.Net.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Okx.Net.Objects.Models.Perpetual
{
    /// <summary>
    /// Okx balance result
    /// </summary>
    public class OkxBalanceResult
    {
        /// <summary>
        /// Details
        /// </summary>
        [JsonProperty("details")]
        public List<OkxBalance> Details { get; set; } = new List<OkxBalance>();

        /// <summary>
        /// Total equity in USD
        /// </summary>
        [JsonProperty("totalEq")]
        public decimal Total { get; set; }
        /// <summary>
        /// Update time of account information
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonProperty("uTime")]
        public DateTime UpdateTime { get; set; }
    }
}
