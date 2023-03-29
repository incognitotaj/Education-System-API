using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Application
{
    internal static class Startup
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            var assembly = Assembly.GetExecutingAssembly();
            return services
                .AddValidatorsFromAssembly(assembly)
                .AddMediatR(assembly);
        }
    }
}
