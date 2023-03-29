using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.OpenApi
{
    internal static class Startup
    {
        internal static IServiceCollection AddOpenApi(this IServiceCollection services, IConfiguration config)
        {
            var settings = config.GetSection(nameof(SwaggerSettings)).Get<SwaggerSettings>();
            if (settings.Enable)
            {
                services.AddEndpointsApiExplorer();
                services.AddSwaggerGen(options =>
                {
                    options.SwaggerDoc(settings.Version,
                        new OpenApiInfo
                        {
                            Version = settings.Version,
                            Title = settings.Title,
                            Description = settings.Description,
                            TermsOfService = new Uri(uriString: settings.TermsOfUseUrl),
                            Contact = new OpenApiContact
                            {
                                Name = settings.ContactName,
                                Url = new Uri(uriString: settings.ContactUrl)
                            },
                            License = new OpenApiLicense
                            {
                                Name = settings.LicenseName,
                                Url = new Uri(uriString: settings.LicenseUrl)
                            }
                        });

                    var securityProvider = config.GetValue<string>("SecuritySettings:Provider");

                    if (securityProvider.Equals("Jwt", StringComparison.OrdinalIgnoreCase))
                    {
                        options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                        {
                            Name = "Authorization",
                            Type = SecuritySchemeType.ApiKey,
                            Scheme = "Bearer",
                            BearerFormat = "JWT",
                            In = ParameterLocation.Header,
                            Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
                        });
                        options.AddSecurityRequirement(new OpenApiSecurityRequirement
                        {
                            {
                                new OpenApiSecurityScheme
                                {
                                    Reference = new OpenApiReference
                                    {
                                        Type = ReferenceType.SecurityScheme,
                                            Id = "Bearer"
                                    }
                                },
                                new string[] {}
                            }
                        });
                    }

                });
            }

            return services;
        }

        internal static IApplicationBuilder UseOpenApi(this IApplicationBuilder app, IConfiguration configuration)
        {
            var corsSettings = configuration.GetValue<bool>("SwaggerSettings:Enable");

            if (corsSettings)
            {
                app
                    .UseSwagger()
                    .UseSwaggerUI(options =>
                    {
                        options.SwaggerEndpoint("/swagger/v1/swagger.json", configuration.GetValue<string>("SwaggerSettings:Title"));
                        options.RoutePrefix = string.Empty;
                        options.DocExpansion(DocExpansion.None);
                    });
            }


            return app;
        }
    }
}
