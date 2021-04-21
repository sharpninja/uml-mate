using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GPS.PlantUml
{
    public static class UmlExtensions
    {
        public const string PLANT_UML_SERVER_URL = "https://www.PlantUml.com/PlantUml/svg/~1";

        public static IConfiguration ConfigureUmlServices(IServiceProvider serviceProvider, IConfigurationRoot configuration)
        {
            serviceProvider.GetRequiredService<UmlService>().Configure(configuration);

            return configuration;
        }

        public static IServiceCollection AddUmlServices(this IServiceCollection services)
        {
            services.AddSingleton<UmlService>();
            services.AddSingleton<UmlOptions>();

            services.AddOptions<UmlOptions>()
                .Configure(options =>
                {
                    options.Url = PLANT_UML_SERVER_URL;
                });

            return services;
        }
    }
}