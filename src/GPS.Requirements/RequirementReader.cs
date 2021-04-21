using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using Microsoft.Extensions.Logging;
using YamlDotNet.Serialization;

namespace GPS.Requirements
{
    public class RequirementReader
    {
        public ILogger<RequirementReader> Logger { get; }
        public string Filename { get; }

        public RequirementReader(ILogger<RequirementReader> logger, string filename)
        {
            Logger = logger;
            Filename = filename;

            ReadRequirement(Filename);
        }

        public IDictionary<string, object> ReadRequirement(string filename = null)
        {
            filename ??= Filename;

            var deserializer = new DeserializerBuilder().Build();

            var requirement = deserializer.Deserialize<IDictionary<string, object>>(File.ReadAllText(filename));
            requirement.TryGetValue("siblings", out var value);

            if (value is not IEnumerable siblings)
            {
                return requirement;
            }

            foreach (var sibling in siblings)
            {
                if(Logger.IsEnabled(LogLevel.Information))
                {
                    Logger.LogInformation($"sibling: {sibling}");
                }
            }

            return requirement;
        }
    }
}
