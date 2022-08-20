using CryptoExchange.Net.Objects;
using System;

namespace Okx.Net.Objects
{
    /// <summary>
    /// Options for the Okx client
    /// </summary>
    public class OkxClientOptions : BaseRestClientOptions
    {
        /// <summary>
        /// Default options for the spot client
        /// </summary>
        public static OkxClientOptions Default { get; set; } = new OkxClientOptions();

        /// <summary>
        /// The default receive window for requests
        /// </summary>
        public TimeSpan ReceiveWindow { get; set; } = TimeSpan.FromSeconds(5);

        private OkxApiClientOptions _perpetualApiOptions = new OkxApiClientOptions(OkxApiAddresses.Default.PerpetualRestClientAddress!)
        {
            AutoTimestamp = true
        };
        /// <summary>
        /// Usd futures API options
        /// </summary>
        public OkxApiClientOptions PerpetualApiOptions
        {
            get => _perpetualApiOptions;
            set => _perpetualApiOptions = new OkxApiClientOptions(_perpetualApiOptions, value);
        }

        /// <summary>
        /// ctor
        /// </summary>
        public OkxClientOptions() : this(Default)
        {
        }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="baseOn">Base the new options on other options</param>
        internal OkxClientOptions(OkxClientOptions baseOn) : base(baseOn)
        {
            if (baseOn == null)
                return;

            ApiCredentials = (OkxApiCredentials?)baseOn.ApiCredentials?.Copy();
            _perpetualApiOptions = new OkxApiClientOptions(baseOn.PerpetualApiOptions, null);
        }
    }

    /// <summary>
    /// Options for the KucoinSocketClient
    /// </summary>
    public class OkxSocketClientOptions : BaseSocketClientOptions
    {
        /// <summary>
        /// Default options for the spot client
        /// </summary>
        public static OkxSocketClientOptions Default { get; set; } = new OkxSocketClientOptions()
        {
            SocketSubscriptionsCombineTarget = 10,
            MaxSocketConnections = 50
        };

        /// <inheritdoc />
        public new OkxApiCredentials? ApiCredentials
        {
            get => (OkxApiCredentials?)base.ApiCredentials;
            set => base.ApiCredentials = value;
        }

        private OkxSocketApiClientOptions _perpetualStreamsOptions = new OkxSocketApiClientOptions(OkxApiAddresses.Default.PerpetualSocketClientAddress!)
        {

        };
        /// <summary>
        /// Futures stream options
        /// </summary>
        public OkxSocketApiClientOptions PerpetualStreamsOptions
        {
            get => _perpetualStreamsOptions;
            set => _perpetualStreamsOptions = new OkxSocketApiClientOptions(_perpetualStreamsOptions, value);
        }

        /// <summary>
        /// ctor
        /// </summary>
        public OkxSocketClientOptions() : this(Default)
        {
        }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="baseOn">Base the new options on other options</param>
        internal OkxSocketClientOptions(OkxSocketClientOptions baseOn) : base(baseOn)
        {
            if (baseOn == null)
                return;

            ApiCredentials = (OkxApiCredentials?)baseOn.ApiCredentials?.Copy();
            _perpetualStreamsOptions = new OkxSocketApiClientOptions(baseOn.PerpetualStreamsOptions, null);
        }
    }

    /// <summary>
    /// Binance API client options
    /// </summary>
    public class OkxApiClientOptions : RestApiClientOptions
    {
        /// <summary>
        /// A manual offset for the timestamp. Should only be used if AutoTimestamp and regular time synchronization on the OS is not reliable enough
        /// </summary>
        public TimeSpan TimestampOffset { get; set; } = TimeSpan.Zero;

        /// <summary>
        /// How often the trade rules should be updated. Only used when TradeRulesBehaviour is not None
        /// </summary>
        public TimeSpan TradeRulesUpdateInterval { get; set; } = TimeSpan.FromMinutes(60);

        /// <summary>
        /// ctor
        /// </summary>
        public OkxApiClientOptions()
        {
        }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="baseAddress"></param>
        internal OkxApiClientOptions(string baseAddress) : base(baseAddress)
        {
        }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="baseOn"></param>
        /// <param name="newValues"></param>
        internal OkxApiClientOptions(OkxApiClientOptions baseOn, OkxApiClientOptions? newValues) : base(baseOn, newValues)
        {
            TimestampOffset = newValues?.TimestampOffset ?? baseOn.TimestampOffset;
            TradeRulesUpdateInterval = newValues?.TradeRulesUpdateInterval ?? baseOn.TradeRulesUpdateInterval;
        }
    }

    /// <summary>
    /// Socket client options
    /// </summary>
    public class OkxSocketApiClientOptions : ApiClientOptions
    {
        /// <inheritdoc />
        public new OkxApiCredentials? ApiCredentials
        {
            get => (OkxApiCredentials?)base.ApiCredentials;
            set => base.ApiCredentials = value;
        }

        /// <summary>
        /// ctor
        /// </summary>
        public OkxSocketApiClientOptions()
        {
        }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="baseAddress"></param>
        internal OkxSocketApiClientOptions(string baseAddress) : base(baseAddress)
        {
        }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="baseOn"></param>
        /// <param name="newValues"></param>
        internal OkxSocketApiClientOptions(OkxSocketApiClientOptions baseOn, OkxSocketApiClientOptions? newValues) : base(baseOn, newValues)
        {
            ApiCredentials = (OkxApiCredentials?)newValues?.ApiCredentials?.Copy() ?? (OkxApiCredentials?)baseOn.ApiCredentials?.Copy();
        }
    }
}
