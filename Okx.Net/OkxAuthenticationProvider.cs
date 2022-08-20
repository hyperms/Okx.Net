using CryptoExchange.Net;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Objects;
using Newtonsoft.Json;
using Okx.Net.Extensions;
using Okx.Net.Objects;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Text;

namespace Okx.Net
{
        /// <inheritdoc />
    public class OkxAuthenticationProvider : AuthenticationProvider
    {
        /// <summary>
        /// ctor
        /// </summary>
        public OkxAuthenticationProvider(OkxApiCredentials credentials) : base(credentials)
        {
        }

        /// <inheritdoc />
        public override void AuthenticateRequest(RestApiClient apiClient, Uri uri, HttpMethod method, Dictionary<string, object> providedParameters, bool auth, ArrayParametersSerialization arraySerialization, HttpMethodParameterPosition parameterPosition, out SortedDictionary<string, object> uriParameters, out SortedDictionary<string, object> bodyParameters, out Dictionary<string, string> headers)
        {
            uriParameters = parameterPosition == HttpMethodParameterPosition.InUri ? new SortedDictionary<string, object>(providedParameters) : new SortedDictionary<string, object>();
            bodyParameters = parameterPosition == HttpMethodParameterPosition.InBody ? new SortedDictionary<string, object>(providedParameters) : new SortedDictionary<string, object>();
            headers = new Dictionary<string, string>();

            if (!auth)
                return;

            uri = uri.SetParameters(uriParameters, arraySerialization);
            headers.Add("OK-ACCESS-KEY", Credentials.Key!.GetString());
            headers.Add("OK-ACCESS-TIMESTAMP", (DateTime.UtcNow.ToUnixTimeMilliSeconds() / 1000.0m).ToString(CultureInfo.InvariantCulture));
            //headers.Add("OK-ACCESS-TIMESTAMP", "1660546131.985");
            headers.Add("OK-ACCESS-PASSPHRASE", ((OkxApiCredentials)Credentials).PassPhrase.GetString());

            var jsonContent = parameterPosition == HttpMethodParameterPosition.InBody ? JsonConvert.SerializeObject(bodyParameters) : string.Empty;
            var signData = headers["OK-ACCESS-TIMESTAMP"] + method + Uri.UnescapeDataString(uri.PathAndQuery) + jsonContent;
            headers.Add("OK-ACCESS-SIGN", SignHMACSHA256(signData, SignOutputType.Base64));
        }
    }
}
