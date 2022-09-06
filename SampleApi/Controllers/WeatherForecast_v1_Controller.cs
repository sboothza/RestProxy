using System;
using System.Collections.Generic;
using System.Linq;

using Contracts;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace SampleApi.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class WeatherForecast_v1_Controller : ControllerBase
	{
		private static readonly string[] _summaries = {
			"FREEZING", "BRACING", "CHILLY", "COOL", "MILD", "WARM", "BALMY", "HOT", "SWELTERING", "SCORCHING"
		};

		private readonly ILogger<WeatherForecastController> _logger;

		public WeatherForecast_v1_Controller(ILogger<WeatherForecastController> logger)
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
			return new()
			{
				Date = DateTime.Now,
				Summary = "summary",
				TemperatureC = index
			};
		}
	}
}
