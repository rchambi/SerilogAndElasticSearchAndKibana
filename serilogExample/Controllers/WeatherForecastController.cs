using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using serilogExample.Services;

namespace serilogExample.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly ITesteService _service;

        public WeatherForecastController(ILogger<WeatherForecastController> logger,ITesteService service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            var complexObject = new WeatherForecast
            {
                Date = DateTime.Now,
                TemperatureC = 25
            };

            //log en el servicio
            var cad =  _service.test();
            _logger.LogInformation("This is an example of a complex object: {@ComplexObject}.", complexObject);

            // CustomOperation();

            _logger.LogInformation("This log should not contain extra properties");
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
