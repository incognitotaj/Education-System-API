using AutoWrapper;
using Infrastructure.ApiVersion;
using Infrastructure.Cors;
using Infrastructure.OpenApi;
using Infrastructure.SecurityHeaders;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class Startup
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services
                // Api Versioning
                .AddApiVersion()

                // Open Api Documentation
                .AddOpenApi(configuration)

                // Cors Policy
                .AddCorsPolicy(configuration)

                // Add Routing
                .AddRouting(configureOptions => { configureOptions.LowercaseUrls = true; });

            return services;
        }


        public static IApplicationBuilder UseInfrastructure(this IApplicationBuilder app, IConfiguration configuration)
        {
            app
                // Enable support for static files
                .UseStaticFiles()

                // Adding Security Header to API Response
                .UseSecurityHeaders(configuration)

                .UseApiResponseAndExceptionWrapper(
                    new AutoWrapperOptions
                    {
                        IsDebug = true,
                        IsApiOnly = true,
                        ShowApiVersion = true,
                        ShowStatusCode = true,
                        ShowIsErrorFlagForSuccessfulResponse = true,
                    }
                )

                // Enable Routing
                .UseRouting()
                
                // Enable Cors Support
                .UseCorsPolicy()
                
                .UseAuthentication()
                
                // Enable Open API Documentation
                .UseOpenApi(configuration);
            return app;
        }

        public static IEndpointRouteBuilder MapEndpoints(this IEndpointRouteBuilder builder)
        {
            builder.MapControllers();
            return builder;
        }

    }
}
