using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Infrastructure.Identity;
using Application.Common.Interfaces;

namespace Infrastructure.Auth;

internal static class Startup
{
    internal static IServiceCollection AddAuth(this IServiceCollection services, IConfiguration config)
    {
        services
            .AddCurrentUser()
            
            // Must add identity before adding auth!
            .AddIdentity();
        services.Configure<SecuritySettings>(config.GetSection(nameof(SecuritySettings)));
        return services;
    }

    private static IServiceCollection AddCurrentUser(this IServiceCollection services) =>
        services
            .AddScoped<CurrentUserMiddleware>()
            .AddScoped<ICurrentUser, CurrentUser>()
            .AddScoped(sp => (ICurrentUserInitializer)sp.GetRequiredService<ICurrentUser>());

}
