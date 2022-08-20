using CryptoExchange.Net.Converters;
using Okx.Net.Enums;
using System.Collections.Generic;

namespace Okx.Net.Converters
{
    internal class MarginModeConverter : BaseConverter<MarginMode>
    {
        public MarginModeConverter() : this(true) { }
        public MarginModeConverter(bool quotes) : base(quotes) { }
        protected override List<KeyValuePair<MarginMode, string>> Mapping => new List<KeyValuePair<MarginMode, string>>
        {
            new KeyValuePair<MarginMode, string>(MarginMode.Cross, "cross"),
            new KeyValuePair<MarginMode, string>(MarginMode.Isolated, "isolated")
        };
    }
}
