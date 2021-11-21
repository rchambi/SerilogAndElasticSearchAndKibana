using Serilog.Core;
using Serilog.Events;

namespace serilogExample.Enrichers
{
    public class CustomEnricher : ILogEventEnricher
    {
        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
            var property = propertyFactory.CreateProperty("Custom Enricher", "Latino .NET Online");
            logEvent.AddPropertyIfAbsent(property);
        }
    }
}
