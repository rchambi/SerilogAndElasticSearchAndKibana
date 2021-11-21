// using Serilog;

using Microsoft.Extensions.Logging;

namespace serilogExample.Services
{
    public class TesteService : ITesteService
    {
            private readonly ILogger<TesteService> _logger;
            // private readonly ILogger _logger;

        public TesteService( ILogger<TesteService> logger)
        {
            _logger = logger;
        }
        public string test()
        {
            var code ="aaaaaa";
            _logger.LogInformation($"This is an example of a complex object: {code}.");
            _logger.LogError("Error---------------------------");
            // _logger.LogInformation("This is an example of a complex object: {@ComplexObject}.", complexObject);
            return "holaaaaaaaaaaaa";           
        }
    }
}