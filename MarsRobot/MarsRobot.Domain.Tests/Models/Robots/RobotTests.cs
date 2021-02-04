using MarsRobot.Domain.Commands.Move;
using MarsRobot.Domain.Commands.Turn;
using MarsRobot.Domain.Contracts.Robots;
using MarsRobot.Domain.Models.Enums;
using MarsRobot.Domain.Models.Robots;
using MarsRobot.Domain.Models.ValueObjects;
using MediatR;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace MarsRobot.Domain.Tests.Models.Robots
{
    public class RobotTests
    {
        private readonly IRobot _robot;
        private readonly Mock<IMediator> _mediatorMock = new Mock<IMediator>();

        public RobotTests()
        {
            _robot = new Robot(_mediatorMock.Object);
        }

        [Fact]
        public async Task Navigate_WhenThereIsAMoveForwardCommand_ShouldMoveForward()
        {
            var commands = "F";
            var initialPosition = _robot.Position;
            var initialDirection = _robot.Direction;
            var command = new MoveForwardCommand { CurrentPosition = initialPosition, Direction = initialDirection };
            var expectedPosition = new Point { X = 1, Y = 2 };
            var result = new MoveResult { NewPosition = expectedPosition };
            _mediatorMock.Setup(m => m.Send(command, It.IsAny<CancellationToken>())).ReturnsAsync(result);

            await _robot.Navigate(commands);

            _mediatorMock.Verify(m => m.Send(command, It.IsAny<CancellationToken>()), Times.Once);
            _mediatorMock.VerifyNoOtherCalls();
            Assert.Equal(expectedPosition, _robot.Position);
        }

        [Fact]
        public async Task Navigate_WhenThereIsATurnRightCommand_ShouldTurnRight()
        {
            var commands = "R";
            var initialDirection = _robot.Direction;
            var expectedDirection = Direction.East;
            var command = new TurnRightCommand { CurrentDirection = initialDirection };
            var result = new TurnResult { NewDirection = expectedDirection };
            _mediatorMock.Setup(m => m.Send(command, It.IsAny<CancellationToken>())).ReturnsAsync(result);

            await _robot.Navigate(commands);

            _mediatorMock.Verify(m => m.Send(command, It.IsAny<CancellationToken>()), Times.Once);
            _mediatorMock.VerifyNoOtherCalls();
            Assert.Equal(expectedDirection, _robot.Direction);
        }

        [Fact]
        public async Task Navigate_WhenThereIsATurnLeftCommand_ShouldTurnLeft()
        {
            var commands = "L";
            var initialDirection = _robot.Direction;
            var expectedDirection = Direction.West;
            var command = new TurnLeftCommand { CurrentDirection = initialDirection };
            var result = new TurnResult { NewDirection = expectedDirection };
            _mediatorMock.Setup(m => m.Send(command, It.IsAny<CancellationToken>())).ReturnsAsync(result);

            await _robot.Navigate(commands);

            _mediatorMock.Verify(m => m.Send(command, It.IsAny<CancellationToken>()), Times.Once);
            _mediatorMock.VerifyNoOtherCalls();
            Assert.Equal(expectedDirection, _robot.Direction);
        }
    }
}