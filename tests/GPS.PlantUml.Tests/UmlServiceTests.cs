using System;
using System.Reflection;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using Microsoft.Extensions.Logging;

namespace GPS.PlantUml.Tests
{
    public class UmlServiceTests : BaseUmlTests
    {
        private ILogger<UmlServiceTests> Logger { get; }
        public UmlServiceTests() : base()
        {
            Logger = GetLogger<UmlServiceTests>();
        }

        [Fact]
        public void GetUmlService()
        {
            Logger.LogInformation($"{MethodBase.GetCurrentMethod()?.Name} Starting.");

            var service = Host.Services.GetService<UmlService>();

            service.Should().NotBeNull();

            Logger.LogInformation($"{MethodBase.GetCurrentMethod()?.Name} Completed.");
        }
    }
}
