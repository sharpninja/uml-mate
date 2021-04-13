using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;

namespace GPS.PlantUml
{
    public abstract class UmlGeneratorBase : IDisposable
    {
        public ILogger Logger { get; }
        public List<string> Fragments { get; }

        protected UmlGeneratorBase(ILogger logger)
        {
            Fragments = new();

            Logger = logger;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
