using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.ComTypes;
using System.Text.RegularExpressions;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Xunit;
using Xunit.Abstractions;

namespace GPS.Requirements.Tests
{
    public class RepositoryServiceTests : BaseRepositoryTests
    {
        private static string _cwd;
        private static List<string> _templateFilenames;

        private static string TemplatesFolder => Path.Combine(
            _cwd ??= System.Environment.CurrentDirectory,
            "app_data/templates");
        private ILogger<RepositoryServiceTests> Logger { get; }
        public RepositoryServiceTests(ITestOutputHelper outputHelper) : base(outputHelper)
        {
            Logger = GetLogger<RepositoryServiceTests>();
        }

        private static List<string> TemplateFilenames => _templateFilenames ??=
            Directory.GetFiles(TemplatesFolder, "*.yaml").ToList();

        public static IEnumerable<object[]> GetTemplates(string filter)
        {
            var regex = new Regex(filter);

            yield return TemplateFilenames.Where(item => regex.IsMatch(item)).Cast<object>().ToArray();
        }

        [Theory]
        [MemberData(nameof(GetTemplates), ".*")]
        public void GetRequirementsByFilename(string filename)
        {
            filename.Should().NotBeNullOrWhiteSpace();

            Logger.LogInformation($"filename: {filename}");

            var reader = new RequirementReader(
                base.Host.Services.GetService<ILogger<RequirementReader>>(), filename);

            reader.Should().NotBeNull();

            var requirements = reader.ReadRequirement();

            requirements.Should().NotBeNull();
        }
    }
}
