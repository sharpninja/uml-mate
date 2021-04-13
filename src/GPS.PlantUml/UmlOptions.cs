using System;
using Microsoft.Extensions.Options;

namespace GPS.PlantUml
{
    public class UmlOptions : IOptions<UmlOptions>
    {
        public UmlOptions()
        {
            Value = this;
        }

        public string Url { get; set; } = string.Empty;
        public Uri Uri => new Uri(Url);

        public UmlOptions Value { get; set; }
    }
}