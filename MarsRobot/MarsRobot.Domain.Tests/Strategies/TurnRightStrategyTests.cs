using MarsRobot.Domain.Commands.Turn;
using MarsRobot.Domain.Contracts.Strategies;
using MarsRobot.Domain.Contracts.ValueObjects;
using MarsRobot.Domain.Models.Enums;
using MarsRobot.Domain.Strategies;
using MediatR;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace MarsRobot.Domain.Tests.Strategies
{
    public class TurnRightStrategyTests
    {
        private readonly Mock<IMediator> _mediatorMock = new();
        private readonly IMovementStrategy _movementStrategy;

        public TurnRightStrategyTests()
        {
            _movementStrategy = new TurnRightStrategy(_mediatorMock.Object);
        }

        [Fact]
        public async Task PerformAction_WhenTurningRight_ShouldJustTurnRight()
        {
            var currentPosition = It.IsAny<IPoint>();
            var currentDirection = Direction.North;
            var command = new TurnRightCommand { CurrentDirection = currentDirection };
            var expectedDirection = Direction.East;
            var result = new TurnResult { NewDirection = expectedDirection };
            _mediatorMock.Setup(m => m.Send(command, It.IsAny<CancellationToken>())).ReturnsAsync(result);

            var actionResult = await _movementStrategy.PerformAction(currentPosition, currentDirection);

            _mediatorMock.Verify(m => m.Send(command, It.IsAny<CancellationToken>()), Times.Once);
            Assert.Equal(currentPosition, actionResult.Position);
            Assert.Equal(expectedDirection, actionResult.Direction);
        }
    }
}
