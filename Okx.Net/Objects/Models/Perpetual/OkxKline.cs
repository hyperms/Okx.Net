using CryptoExchange.Net.Converters;
using Newtonsoft.Json;
using System;

namespace Okx.Net.Objects.Models.Perpetual
{
    /// <summary>
    /// kline detail
    /// </summary>
    [JsonConverter(typeof(ArrayConverter))]
    public class OkxKline
    {
        /// <summary>
        /// Opening time of the candlestick
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [ArrayProperty(0)]
        public DateTime OpenTime { get; set; }
        /// <summary>
        /// Open price
        /// </summary>
        [ArrayProperty(1)]
        public decimal OpenPrice { get; set; }
        /// <summary>
        /// highest price
        /// </summary>
        [ArrayProperty(2)]
        public decimal HighPrice { get; set; }
        /// <summary>
        /// Lowest price
        /// </summary>
        [ArrayProperty(3)]
        public decimal LowPrice { get; set; }
        /// <summary>
        /// Close price
        /// </summary>
        [ArrayProperty(4)]
        public decimal ClosePrice { get; set; }
        /// <summary>
        /// Trading volume, with a unit of contract
        /// </summary>
        [ArrayProperty(5)]
        public decimal Volume { get; set; }

    }
}
