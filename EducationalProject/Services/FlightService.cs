using EducationalProject.Models;
using Microsoft.Extensions.Configuration;
using System.Text.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.Extensions.Options;
using EducationalProject.Options;

namespace EducationalProject
{
    public class FlightService
    {

        private readonly string apiKey;
        private readonly string apiSecret;
        private readonly HttpClient http;
        private readonly IOptionsMonitor<AccessTokenOptions> _accessTokenOptions;

        public FlightService(IConfiguration config,
            HttpClient httpClient,
            IOptionsMonitor<AccessTokenOptions> accessTokenOptions)
        {
            apiKey = config.GetValue<string>("AmadeusAPI:APIKey");
            apiSecret = config.GetValue<string>("AmadeusAPI:APISecret");
            http = httpClient;
            _accessTokenOptions = accessTokenOptions;
        }

        public string Get()
        {
            return _accessTokenOptions.CurrentValue.AccessToken;
        }

        public async Task<string> GetAccesToken(CancellationToken token)
        {
            var message = new HttpRequestMessage(HttpMethod.Post, "/v1/security/oauth2/token")
            {
                Content = new StringContent(
                $"grant_type=client_credentials&client_id={apiKey}&client_secret={apiSecret}",
                Encoding.UTF8, "application/x-www-form-urlencoded"
            )
            };

            var res = await http.SendAsync(message,token);

            var content = await res.Content.ReadAsStringAsync();

            var oauthResult = JsonSerializer.Deserialize<OAuthResult>(content);

            return oauthResult.access_token;
        }

    }
}
