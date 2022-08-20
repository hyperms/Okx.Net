using CryptoExchange.Net.Converters;
using Okx.Net.Enums;
using System.Collections.Generic;

namespace Okx.Net.Converters
{
    internal class KlineIntervalConverter : BaseConverter<KlineInterval>
    {
        public KlineIntervalConverter() : this(true) { }
        public KlineIntervalConverter(bool quotes) : base(quotes) { }
        protected override List<KeyValuePair<KlineInterval, string>> Mapping => new List<KeyValuePair<KlineInterval, string>>
        {
            new KeyValuePair<KlineInterval, string>(KlineInterval.OneMinute, "1m"),
            new KeyValuePair<KlineInterval, string>(KlineInterval.ThreeMinute, "3m"),
            new KeyValuePair<KlineInterval, string>(KlineInterval.FiveMinutes, "5m"),
            new KeyValuePair<KlineInterval, string>(KlineInterval.FifteenMinutes, "15m"),
            new KeyValuePair<KlineInterval, string>(KlineInterval.ThirtyMinutes, "30m"),
            new KeyValuePair<KlineInterval, string>(KlineInterval.OneHour, "1H"),
            new KeyValuePair<KlineInterval, string>(KlineInterval.TwoHours, "2H"),
            new KeyValuePair<KlineInterval, string>(KlineInterval.FourHours, "4H"),
            new KeyValuePair<KlineInterval, string>(KlineInterval.SixHours, "6H"),
            new KeyValuePair<KlineInterval, string>(KlineInterval.TwelveHours, "12H"),
            new KeyValuePair<KlineInterval, string>(KlineInterval.OneDay, "1D"),
            new KeyValuePair<KlineInterval, string>(KlineInterval.TwoDay, "2D"),
            new KeyValuePair<KlineInterval, string>(KlineInterval.ThreeDay, "3D"),
            new KeyValuePair<KlineInterval, string>(KlineInterval.OneWeek, "1W"),
            new KeyValuePair<KlineInterval, string>(KlineInterval.OneMonth, "1M"),
            new KeyValuePair<KlineInterval, string>(KlineInterval.ThreeMonth, "3M"),
            new KeyValuePair<KlineInterval, string>(KlineInterval.SixMonth, "6M"),
            new KeyValuePair<KlineInterval, string>(KlineInterval.OneYear, "1Y"),
        };
    }
}
