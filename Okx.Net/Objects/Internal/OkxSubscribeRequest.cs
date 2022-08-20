using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace Okx.Net.Objects.Internal
{
    internal class OkxRequest
    {
        [JsonProperty("op")]
        public string Operation { get; set; }

        [JsonProperty("args")]
        public List<OkxRequestArgument> Arguments { get; set; }

        public OkxRequest(string op, params OkxRequestArgument[] args)
        {
            Operation = op;
            Arguments = args.ToList();
        }

        public OkxRequest(string op, IEnumerable<OkxRequestArgument> args)
        {
            Operation = op;
            Arguments = args.ToList();
        }

        public OkxRequest(string op, string channel)
        {
            Operation = op;
            Arguments = new List<OkxRequestArgument>();
            Arguments.Add(new OkxRequestArgument(channel));
        }

        public OkxRequest(string op, string channel, string instrumentId)
        {
            Operation = op;
            Arguments = new List<OkxRequestArgument>();
            Arguments.Add(new OkxRequestArgument(channel, instrumentId));
        }

        public OkxRequest(string op, string channel, string underlying, string instrumentId)
        {
            Operation = op;
            Arguments = new List<OkxRequestArgument>();
            Arguments.Add(new OkxRequestArgument(channel, underlying, instrumentId));
        }
    }

    internal class OkxRequestArgument
    {
        [JsonProperty("channel")]
        public string Channel { get; set; } = string.Empty;

        [JsonProperty("uly", NullValueHandling = NullValueHandling.Ignore)]
        public string Underlying { get; set; } = string.Empty;

        [JsonProperty("instId", NullValueHandling = NullValueHandling.Ignore)]
        public string InstrumentId { get; set; } = string.Empty;


        public OkxRequestArgument()
        {
        }

        public OkxRequestArgument(string channel)
        {
            if (!string.IsNullOrEmpty(channel)) Channel = channel;
        }

        public OkxRequestArgument(string channel, string instrumentId)
        {
            if (!string.IsNullOrEmpty(channel)) Channel = channel;
            if (!string.IsNullOrEmpty(instrumentId)) InstrumentId = instrumentId;
        }

        public OkxRequestArgument(string channel, string underlying, string instrumentId)
        {
            if (!string.IsNullOrEmpty(channel)) Channel = channel;
            if (!string.IsNullOrEmpty(underlying)) Underlying = underlying;
            if (!string.IsNullOrEmpty(instrumentId)) InstrumentId = instrumentId;
        }
    }
}
