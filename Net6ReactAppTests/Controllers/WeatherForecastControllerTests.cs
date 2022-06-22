using Xunit;
using Net6ReactApp.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Microsoft.Extensions.Logging;

namespace Net6ReactApp.Controllers.Tests
{
	public class WeatherForecastControllerTests
	{
		private static readonly List<string> Summaries = new List<string>(new string[] { "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching" });

		[Fact()]
		public void WeatherForecastControllerTest()
		{
			// Arrange: Set up a mock ILogger
			Mock<NLog.ILogger> loggerMock = new();
			loggerMock.Setup(mock => mock.Log(NLog.LogLevel.Trace, "WeatherForecastController Initializing"));

			// Act: Initialize a WeatherForecastController using the mock
			WeatherForecastController controller = new WeatherForecastController(loggerMock.Object);

			// Assert: Verify the controller was initialized, and the logger was used.
			Assert.NotNull(controller);
			loggerMock.VerifyAll();
		}

		[Fact()]
		public void GetTest()
		{
			// Arrange: Set up a mock ILogger and create a controller
			Mock<NLog.ILogger> loggerMock = new();
			loggerMock.Setup(mock => mock.Log(NLog.LogLevel.Trace, "WeatherForecastController Initializing"));
			WeatherForecastController controller = new WeatherForecastController(loggerMock.Object);
			DateTime today = DateTime.Now.Date;

			// Act: Call the Get method
			var forecasts = controller.Get().ToList();

			// Assert: Verify the results
			Assert.NotNull(forecasts);

			int dateOffset = 1;
			foreach (var forecast in forecasts)
			{

				Assert.NotNull(forecast.Summary);
				Assert.True(Summaries.Contains(forecast.Summary));
				Assert.True(forecast.Date.Date.Equals(today.AddDays(dateOffset++).Date));
				Assert.True(forecast.TemperatureC >= -20 && forecast.TemperatureC <= 55);
			}

			loggerMock.VerifyAll();
		}
	}
}