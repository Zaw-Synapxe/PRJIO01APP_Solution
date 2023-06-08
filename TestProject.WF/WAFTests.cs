using Auth.API.Controllers;
using Auth.API.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Moq;

namespace TestProject.WF
{
    public class WAFTests
    {
        [Fact]
        public void Get_Weather_Test()
        {
            // Arrange
            Mock<ILogger<WeatherForecastController>> loggerMock = new();
            var controller = new WeatherForecastController(loggerMock.Object);

            // Act
            IEnumerable<WeatherForecast> result = controller.Get();

            // Assert
            Assert.Equal(5, result.Count());
        }

        [Fact]
        public async Task Get_Weather_Integration_Test()
        {
            // Arrange
            var builder = new WebHostBuilder().UseStartup<Program>();
            TestServer server = new(builder);
            HttpClient client = server.CreateClient();

            // Act
            var response = await client.GetAsync("/GetWeatherForecast");

            // Assert
            response.EnsureSuccessStatusCode();
        }


        //
    }
}