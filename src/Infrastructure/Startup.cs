using AutoWrapper;
using Infrastructure.ApiVersion;
using Infrastructure.Auth;
using Infrastructure.Common;
using Infrastructure.Cors;
using Infrastructure.Middleware;
using Infrastructure.OpenApi;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Initialization;
using Infrastructure.SecurityHeaders;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Infrastructure
{
    public static class Startup
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services
                // Api Versioning
                .AddApiVersion()

                // Adding Database
                .AddPersistence(configuration)

                // Mediator for CQRS
                .AddMediatR(Assembly.GetExecutingAssembly())

                // Adding Authentication
                .AddAuth(configuration)

                // Open Api Documentation
                .AddOpenApi(configuration)

                // Cors Policy
                .AddCorsPolicy(configuration)

                .AddExceptionMiddleware()

                // Add Routing
                .AddRouting(configureOptions => { configureOptions.LowercaseUrls = true; })
                
                .AddServices();

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

                .UseExceptionMiddleware()
               
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

        public static async Task InitializeDatabasesAsync(this IServiceProvider services, CancellationToken cancellationToken = default)
        {
            // Create a new scope to retrieve scoped services
            using var scope = services.CreateScope();

            await scope.ServiceProvider.GetRequiredService<IDatabaseInitializer>()
                .InitializeDatabasesAsync(cancellationToken);
        }

    }
}
