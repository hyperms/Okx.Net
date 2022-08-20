using Newtonsoft.Json;

namespace Okx.Net.Objects.Internal
{
    /// <summary>
    /// Okx stream subscribe
    /// </summary>
    public class OkxSubscribeResponse
    {
        /// <summary>
        /// On success
        /// </summary>
        public bool Success
        {
            get
            {
                return
                    string.IsNullOrEmpty(ErrorCode)
                    || ErrorCode.Trim() == "0";
            }
        }

        /// <summary>
        /// Event of stream
        /// </summary>
        [JsonProperty("event")]
        public string Event { get; set; } = string.Empty;
        /// <summary>
        /// code of result
        /// </summary>
        [JsonProperty("code")]
        public string ErrorCode { get; set; } = string.Empty;
        /// <summary>
        /// message of result
        /// </summary>
        [JsonProperty("msg")]
        public string ErrorMessage { get; set; } = string.Empty;
    }
    /// <summary>
    /// Okx stream update
    /// </summary>
    public class OkxSocketUpdateResponse<T> : OkxSubscribeResponse
    {
        /// <summary>
        /// Response data
        /// </summary>
        [JsonProperty("data")]
        public T Data { get; set; } = default!;
    }
}
