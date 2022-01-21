using EducationalProject.BackgroundServices;
using EducationalProject.Middleware;
using EducationalProject.Models;
using EducationalProject.Options;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationalProject
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddHttpClient<FlightService>(client =>
                client.BaseAddress = new Uri("https://test.api.amadeus.com"));

            services.Configure<ServiceAvailableConfiguration>(Configuration.GetSection("AppServices"));
            services.Configure<AccessTokenOptions>(Configuration.GetSection("Tokens"));

            services.AddHostedService<AccessTokenService>();

        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMiddleware<ServiceAvailableMiddleware>();
            //app.UseHttpsRedirection();

            app.UseRouting();

            //app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
