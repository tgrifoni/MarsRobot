using MarsRobot.Domain.Commands.Move;
using MarsRobot.Domain.Contracts.Strategies;
using MarsRobot.Domain.Models.Enums;
using MarsRobot.Domain.Models.ValueObjects;
using MarsRobot.Domain.Strategies;
using MediatR;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace MarsRobot.Domain.Tests.Strategies
{
    public class MoveForwardStrategyTests
    {
        private readonly Mock<IMediator> _mediatorMock = new();
        private readonly IMovementStrategy _movementStrategy;

        public MoveForwardStrategyTests()
        {
            _movementStrategy = new MoveForwardStrategy(_mediatorMock.Object);
        }

        [Fact]
        public async Task PerformAction_WhenMovingForward_ShouldMoveForwardInCurrentDirection()
        {
            var currentPosition = new Point { X = 1, Y = 1 };
            var currentDirection = Direction.North;
            var command = new MoveForwardCommand { CurrentPosition = currentPosition, Direction = currentDirection };
            var expectedPosition = new Point { X = 1, Y = 2 };
            var result = new MoveResult { NewPosition = expectedPosition };
            _mediatorMock.Setup(m => m.Send(command, It.IsAny<CancellationToken>())).ReturnsAsync(result);

            var actionResult = await _movementStrategy.PerformAction(currentPosition, currentDirection);

            _mediatorMock.Verify(m => m.Send(command, It.IsAny<CancellationToken>()), Times.Once);
            Assert.Equal(expectedPosition, actionResult.Position);
            Assert.Equal(currentDirection, actionResult.Direction);
        }
    }
}
