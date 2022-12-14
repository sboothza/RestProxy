using Contracts;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleApi.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class WeatherForecastController : ControllerBase
	{
		private static readonly string[] _summaries = new[]
		{
			"Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
		};

		private readonly ILogger<WeatherForecastController> _logger;

		public WeatherForecastController(ILogger<WeatherForecastController> logger)
		{
			_logger = logger;
		}

		[HttpGet]
		public IEnumerable<WeatherForecast> Get()
		{
			var rng = new Random();
			return Enumerable.Range(1, 5)
							 .Select(index => new WeatherForecast
							 {
								 Date = DateTime.Now.AddDays(index),
								 TemperatureC = rng.Next(-20, 55),
								 Summary = _summaries[rng.Next(_summaries.Length)]
							 })
							 .ToArray();
		}

		[HttpGet]
		[Route("single")]
		public WeatherForecast GetSingle(int index)
		{
			return new WeatherForecast 
			{ 
				Date = DateTime.Now, 
				Summary = "summary", 
				TemperatureC = index 
			};
		}
	}
}
