using EducationalProject.BackgroundServices;
using EducationalProject.Middleware;
using EducationalProject.Models;
using EducationalProject.Options;
using EducationalProject.Services;
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

            services.AddHttpClient<AccessTokenProviderService>(client =>
            {
                client.BaseAddress = new Uri("https://test.api.amadeus.com");
                client.Timeout = new TimeSpan(0, 0, 0,0,100);
            });
            
            services.Configure<ServiceAvailableOptions>(Configuration.GetSection("AppServices"));
            services.Configure<AccessTokenOptions>(Configuration.GetSection("Tokens"));
            services.Configure<AmadeusAPIOptions>(Configuration.GetSection("AmadeusAPI"));

            services.AddHostedService<AccessTokenService>();

            services.AddScoped<FlightService>();

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
