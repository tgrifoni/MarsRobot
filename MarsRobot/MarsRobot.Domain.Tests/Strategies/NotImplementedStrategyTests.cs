using MarsRobot.Domain.Contracts.Strategies;
using MarsRobot.Domain.Contracts.ValueObjects;
using MarsRobot.Domain.Models.Enums;
using MarsRobot.Domain.Strategies;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace MarsRobot.Domain.Tests.Strategies
{
    public class NotImplementedStrategyTests
    {
        private readonly IMovementStrategy _movementStrategy;

        public NotImplementedStrategyTests()
        {
            _movementStrategy = new NotImplementedStrategy();
        }

        [Fact]
        public async Task PerformAction_WhenStrategyIsNotImplemented_ShouldAlwaysReturnTheSamePositionAndDirection()
        {
            var currentPosition = It.IsAny<IPoint>();
            var currentDirection = It.IsAny<Direction>();

            var result = await _movementStrategy.PerformAction(currentPosition, currentDirection);

            Assert.Equal(currentPosition, result.Position);
            Assert.Equal(currentDirection, result.Direction);
        }
    }
}
