namespace Okx.Net.Objects
{
    /// <summary>
    /// Api addresses
    /// </summary>
    public class OkxApiAddresses
    {
        /// <summary>
        /// The address used by the OkxClient for the Perpetual API
        /// </summary>
        public string? PerpetualRestClientAddress { get; set; }
        /// <summary>
        /// The address used by the OkxSocketClient for the Perpetual API
        /// </summary>
        public string? PerpetualSocketClientAddress { get; set; }

        /// <summary>
        /// The default addresses to connect to the okx.com API
        /// </summary>
        public static OkxApiAddresses Default = new OkxApiAddresses
        {
            PerpetualRestClientAddress = "https://www.okx.com/api",
            PerpetualSocketClientAddress = "wss://ws.okex.com:8443/ws/v5/public",
        };

        /// <summary>
        /// The addresses to connect to the binance testnet
        /// </summary>
        public static OkxApiAddresses TestNet = new OkxApiAddresses
        {
            PerpetualRestClientAddress = "https://www.okx.com/api",
            PerpetualSocketClientAddress = "wss://ws.okex.com:8443/ws",
        };
    }
}
