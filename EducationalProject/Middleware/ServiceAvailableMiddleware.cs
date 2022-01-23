using EducationalProject.Options;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;

namespace EducationalProject.Middleware
{
    public class ServiceAvailableMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IOptionsMonitor<ServiceAvailableOptions> _serviceAvailableOptions;

        public ServiceAvailableMiddleware(RequestDelegate next,IOptionsMonitor<ServiceAvailableOptions> options)
        {
            _next = next;
            _serviceAvailableOptions = options;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            bool serviceAvailablity = _serviceAvailableOptions.CurrentValue.ServiceAvailable;

            if(!serviceAvailablity)
            {
                httpContext.Response.StatusCode = 503;
                await httpContext.Response.WriteAsync("Service not available");
                return;
            }

            await _next(httpContext);

        }
    }
}
