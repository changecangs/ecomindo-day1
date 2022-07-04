using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ecomindoDayOne.Controllers
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

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }
        /// <summary>
        /// Getting random weather forecast
        /// </summary>
        /// <response code="200">Successful get r</response>
        /// <response code="404">Not Found</response>
        [HttpGet]
        [ProducesResponseType(404)]
        [ProducesResponseType(typeof(WeatherForecast), 200)]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }
        [HttpGet("{days_after}")]
        public WeatherForecast GetDayLater(int days_after)
        {
            var rng = new Random();
            return new WeatherForecast
            {
                Date = DateTime.Now.AddDays(days_after),
                TemperatureC = rng.Next(-20, 55),
                Summary = "Weather Summary in next " + days_after.ToString() + " days are " + Summaries[rng.Next(Summaries.Length)]
            };
        }
        [HttpPost]
        public string[] Update(int id, string update_summary)
        {
            string[] example_array = Summaries.ToArray();
            if (id == 0 || id > example_array.Length)
            {
                return example_array;
            } else
            {
                example_array[id - 1] = update_summary;
                return example_array;
            }

        }
        [HttpDelete]
        public string[] Delete(int id)
        {
            string[] example_array = Summaries.ToArray();
            example_array = example_array.Where((source, index) => index != id).ToArray();


            return example_array;
        }
    }
}
