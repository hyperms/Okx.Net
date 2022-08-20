using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Okx.Net.Objects.Internal
{
    internal class OkxToken
    {
        public string Token { get; set; } = string.Empty;
        [JsonProperty("instanceServers")]
        public IEnumerable<OkxInstanceServer> Servers { get; set; } = Array.Empty<OkxInstanceServer>();
    }

    internal class OkxInstanceServer
    {
        public int PingInterval { get; set; }
        public string Endpoint { get; set; } = string.Empty;
        public string Protocol { get; set; } = string.Empty;
        public bool Encrypt { get; set; }
        public int PingTimeout { get; set; }
    }
}
