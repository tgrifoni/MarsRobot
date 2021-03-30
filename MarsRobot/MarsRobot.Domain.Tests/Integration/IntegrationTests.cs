using MarsRobot.ConsoleApp;
using MarsRobot.Domain.Contracts.Robots;
using MarsRobot.Domain.Models.Enums;
using MarsRobot.Domain.Models.ValueObjects;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using Xunit;

namespace MarsRobot.Domain.Tests.Integration
{
    public class IntegrationTests
    {
        [Theory]
        [InlineData(1, 1, "A", 1, 1, Direction.North)]
        [InlineData(5, 5, "FFRFLFLF", 1, 4, Direction.West)]
        [InlineData(5, 5, "FFRFLFLFF", 1, 4, Direction.West)]
        [InlineData(1, 4, "FFRFLFLFL", 1, 4, Direction.South)]
        [InlineData(4, 4, "FFRFLFLFR", 1, 4, Direction.North)]
        [InlineData(4, 3, "RFFLFLFRFRFF", 4, 3, Direction.East)]
        [InlineData(6, 5, "RFFFLFRFLFLFFRFFRFFRF", 5, 4, Direction.South)]
        [InlineData(6, 5, "RFFFLFRFLFLFFRFFRFFRFR", 5, 4, Direction.West)]
        public async Task Navigate_WhenCalled_ShouldNavigateProperly(int plateauX, int plateauY, string commands, int expectedXAfterNavigating, int expectedYAfterNavigating, Direction expectedDirectionAfterNavigating)
        {
            var plateau = new Point { X = plateauX, Y = plateauY };
            using var host = HostBuilderFactory.CreateHostBuilder(null, plateau).Build();
            var robot = host.Services.GetRequiredService<IRobot>();
            await host.StopAsync();
            var expectedPosition = new Point { X = expectedXAfterNavigating, Y = expectedYAfterNavigating };

            await robot.Navigate(commands);

            Assert.Equal(expectedDirectionAfterNavigating, robot.Direction);
            Assert.Equal(expectedPosition.X, robot.Position.X);
            Assert.Equal(expectedPosition.Y, robot.Position.Y);
        }
    }
}