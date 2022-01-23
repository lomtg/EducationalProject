using EducationalProject.Helpers;
using EducationalProject.Models;
using EducationalProject.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace EducationalProject.Services
{
    public class AccessTokenProviderService
    {
        private readonly ILogger<AccessTokenProviderService> _logger;
        private readonly AmadeusAPIOptions amadeusAPIOptions;
        private readonly HttpClient _httpClient;
        public AccessTokenProviderService(
            ILogger<AccessTokenProviderService> logger,
            IOptions<AmadeusAPIOptions> _amadeusAPIOptions,
            HttpClient httpClient)
        {
            _logger = logger;
            amadeusAPIOptions = _amadeusAPIOptions.Value;
            _httpClient = httpClient;
        }

        public async Task<string> GetAccesToken(CancellationToken token)
        {
            try
            {

            var res = await _httpClient.SendAsync(RequestMessage.AccessTokenRequestMessage(amadeusAPIOptions.ApiKey,amadeusAPIOptions.ApiSecret), token);

            res.EnsureSuccessStatusCode();

            var stream = await res.Content.ReadAsStreamAsync();

            var oauthResult = await JsonSerializer.DeserializeAsync<OAuthResult>(stream);

            return oauthResult.access_token;
            }
            catch(Exception ex)
            {
                _logger.LogInformation(ex.Message);
                return "";
            }
        }
    }
}
