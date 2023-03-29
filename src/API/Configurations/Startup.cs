namespace API.Configurations
{
    public static class Startup
    {
        public static ConfigureHostBuilder AddConfigurations(this ConfigureHostBuilder builder)
        {
            builder.ConfigureAppConfiguration((context, configBuilder) =>
            {
                var configurationDirectory = "Configurations";
                var env = context.HostingEnvironment;

                configBuilder
                    .AddJsonFile(path: "appsettings.json", optional: false, reloadOnChange: true)
                    .AddJsonFile(path: $"{configurationDirectory}/cors.json", optional: false, reloadOnChange: true)
                    .AddJsonFile(path: $"{configurationDirectory}/cors.{env}.json", optional: true, reloadOnChange: true)
                    .AddJsonFile(path: $"{configurationDirectory}/database.json", optional: false, reloadOnChange: true)
                    .AddJsonFile(path: $"{configurationDirectory}/database.{env}.json", optional: true, reloadOnChange: true)
                    .AddJsonFile(path: $"{configurationDirectory}/openapi.json", optional: false, reloadOnChange: true)
                    .AddJsonFile(path: $"{configurationDirectory}/openapi.{env}.json", optional: true, reloadOnChange: true)
                    .AddJsonFile(path: $"{configurationDirectory}/security.json", optional: false, reloadOnChange: true)
                    .AddJsonFile(path: $"{configurationDirectory}/security.{env}.json", optional: true, reloadOnChange: true)
                    .AddJsonFile(path: $"{configurationDirectory}/securityheaders.json", optional: false, reloadOnChange: true)
                    .AddJsonFile(path: $"{configurationDirectory}/securityheaders.{env}.json", optional: true, reloadOnChange: true)
                    .AddEnvironmentVariables();
            });

            return builder;
        }
    }
}
