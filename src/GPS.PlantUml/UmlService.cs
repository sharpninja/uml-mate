using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace GPS.PlantUml
{
    public class UmlService
    {
        internal IServiceProvider ServiceProvider { get; }
        internal IConfiguration Configuration { get; set; }
        internal IOptions<UmlOptions> UmlOptions { get; set; }

        public UmlService(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
            Configuration = Configure(null)
                ?? throw new ApplicationException("Application is not configured.");
            UmlOptions ??= ServiceProvider.GetRequiredService<UmlOptions>();
        }

        public TGenerator GetGenerator<TGenerator>()
            where TGenerator : UmlGeneratorBase
        {
            return ServiceProvider.GetService<TGenerator>() 
                ?? throw new ApplicationException($"Unknown type: {typeof(TGenerator).Name}");
        }

        public IConfiguration Configure(IConfigurationRoot? configuration)
        {
            configuration ??= ServiceProvider.GetRequiredService<IConfigurationRoot>();

            UmlOptions = ServiceProvider.GetRequiredService<UmlOptions>();

            return Configuration = configuration;
        }

    }
}