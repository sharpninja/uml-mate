using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Xunit.Abstractions;

namespace GPS.Requirements.Tests
{
    public abstract class BaseRepositoryTests
    {
        public ITestOutputHelper OutputHelper { get; }
        private const string ASPNETCORE_ENVIRONMENT = "ASPNETCORE_ENVIRONMENT";
        private const string PRODUCTION = "Production";
        protected IHost Host { get; }
        protected string Environment { get; private set; }

        protected BaseRepositoryTests(ITestOutputHelper outputHelper)
        {
            OutputHelper = outputHelper;
            var builder = new HostBuilder()
                .ConfigureAppConfiguration(configurationBuilder =>
                {
                    configurationBuilder.AddJsonFile(source =>
                    {
                        source.Optional = false;
                        source.Path = "appSettings.json";
                        source.ReloadOnChange = true;
                    });

                    Environment = "Development";
                        // System.Environment.GetEnvironmentVariable(ASPNETCORE_ENVIRONMENT)
                        // ?? PRODUCTION;

                    if (Environment != PRODUCTION)
                    {
                        configurationBuilder.AddJsonFile(source =>
                        {
                            source.Optional = true;
                            source.Path = "appSettings.Development.json";
                            source.ReloadOnChange = true;
                        });
                    }
                })
                .ConfigureServices((bldr, collection) =>
                {
                    collection.AddLogging(loggingBuilder => loggingBuilder.AddXUnit(OutputHelper));
                    collection.AddSingleton(bldr.Configuration);
                    collection.AddTransient(provider => provider.GetRequiredService<IConfiguration>() as IConfigurationRoot);
                })
                .ConfigureLogging(loggingBuilder =>
                {
                    if (Environment != PRODUCTION)
                    {
                        loggingBuilder.AddDebug();
                        loggingBuilder.AddFilter(level => level >= LogLevel.Debug);
                    }
                    else
                    {
                        loggingBuilder.AddFilter(level => level >= LogLevel.Warning);
                    }
                });

            Host = builder.Build();
        }

        protected ILogger<TTests> GetLogger<TTests>()
            => Host.Services.GetRequiredService<ILogger<TTests>>();
    }
}
