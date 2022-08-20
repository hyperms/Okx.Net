using Newtonsoft.Json;

namespace Okx.Net.Objects.Internal
{
    internal class OkxResult<T>
    {
        [JsonProperty("code")]
        public int Code { get; set; }
        [JsonProperty("msg")]
        public string? Message { get; set; }

        public T Data { get; set; } = default!;
    }
}
