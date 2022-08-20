using CryptoExchange.Net.Converters;
using Okx.Net.Enums;
using System.Collections.Generic;

namespace Okx.Net.Converters
{
    internal class PositionSideConverter : BaseConverter<PositionSide>
    {
        public PositionSideConverter() : this(true) { }
        public PositionSideConverter(bool quotes) : base(quotes) { }
        protected override List<KeyValuePair<PositionSide, string>> Mapping => new List<KeyValuePair<PositionSide, string>>
        {
            new KeyValuePair<PositionSide, string>(PositionSide.Long, "long"),
            new KeyValuePair<PositionSide, string>(PositionSide.Short, "short")
        };
    }
}
