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
        private readonly IOptionsMonitor<AccessTokenOptions> _accessTokenOptions;

        public FlightService(IOptionsMonitor<AccessTokenOptions> accessTokenOptions)
        {
            _accessTokenOptions = accessTokenOptions;
        }

        public string Get()
        { 
            return _accessTokenOptions.CurrentValue.AccessToken;
        }

    }
}
