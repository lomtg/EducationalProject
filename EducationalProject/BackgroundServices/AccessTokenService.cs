using EducationalProject.Options;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Threading;
using System.Threading.Tasks;

namespace EducationalProject.BackgroundServices
{
    public class AccessTokenService : BackgroundService
    {
        private readonly FlightService _flightService;
        private readonly ILogger<AccessTokenService> _logger;
        private readonly IOptionsMonitor<ServiceAvailableConfiguration> _serviceAvailableOptions;
        private readonly IOptionsMonitor<AccessTokenOptions> _accessTokenOptions;

        public AccessTokenService(FlightService flightService,
            ILogger<AccessTokenService> logger,
            IOptionsMonitor<ServiceAvailableConfiguration> serviceAvailableOptions,
            IOptionsMonitor<AccessTokenOptions> accessTokenOptions)
        {
            _flightService = flightService;
            _logger = logger;
            _serviceAvailableOptions = serviceAvailableOptions;
            _accessTokenOptions = accessTokenOptions;
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Hosted service shutting down");
            return base.StopAsync(cancellationToken);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while(!stoppingToken.IsCancellationRequested)
            {
                var token = await _flightService.GetAccesToken(stoppingToken);

                if(!string.IsNullOrEmpty(token))
                {
                    _serviceAvailableOptions.CurrentValue.ServiceAvailable = true;
                    _accessTokenOptions.CurrentValue.AccessToken = token;
                }
                else
                {
                    _serviceAvailableOptions.CurrentValue.ServiceAvailable = false; 
                }

                await Task.Delay(10000,stoppingToken);
            }

        }
    }
}
