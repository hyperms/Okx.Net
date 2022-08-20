using CryptoExchange.Net.Converters;
using Newtonsoft.Json;
using System;

namespace Okx.Net.Objects.Models
{
    /// <summary>
    /// Okx system time
    /// </summary>
    public class OkxSystemTime
    {
        /// <summary>
        /// System time
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonProperty("ts")]
        public DateTime SystemTime { get; set; }
    }
}
