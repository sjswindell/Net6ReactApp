using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NLog;

namespace Net6ReactApp.Controllers
{
	[Authorize]
	[ApiController]
	[Route("[controller]")]
	public class WeatherForecastController : ControllerBase
	{
		private static readonly string[] Summaries = new[]
		{
			"Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
		};

		private readonly NLog.ILogger _logger;

		public WeatherForecastController(NLog.ILogger logger)
		{
			_logger = logger;
			_logger.Log(NLog.LogLevel.Trace, "WeatherForecastController Initializing");
		}

		[HttpGet]
		public IEnumerable<WeatherForecast> Get()
		{
			return Enumerable.Range(1, 5).Select(index => new WeatherForecast
			{
				Date = DateTime.Now.AddDays(index),
				TemperatureC = Random.Shared.Next(-20, 55),
				Summary = Summaries[Random.Shared.Next(Summaries.Length)]
			})
			.ToArray();
		}
	}
}