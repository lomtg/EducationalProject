using EducationalProject.Middleware;
using Microsoft.AspNetCore.Builder;

namespace EducationalProject.MiddlewareRegistration
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder CheckServiceAvailablity(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ServiceAvailableMiddleware>();
        }
    }
}
