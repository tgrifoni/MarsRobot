using MarsRobot.Domain.Commands.Move;
using MarsRobot.Domain.Contracts.Validators;
using MarsRobot.Domain.Models.Enums;
using MarsRobot.Domain.Models.ValueObjects;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace MarsRobot.Domain.Tests.Commands.Move
{
    public class MoveCommandHandlerTests
    {
        private readonly Mock<IMoveValidator> _moveValidatorMock = new Mock<IMoveValidator>();
        private readonly MoveCommandHandler _moveCommandHandler;

        public MoveCommandHandlerTests()
        {
            _moveCommandHandler = new MoveCommandHandler(_moveValidatorMock.Object);
        }

        [Theory]
        [InlineData(Direction.North, 1, 1, 1, 2)]
        [InlineData(Direction.South, 1, 2, 1, 1)]
        [InlineData(Direction.East, 1, 1, 2, 1)]
        [InlineData(Direction.West, 2, 1, 1, 1)]
        public async Task MoveForward_WhenMoveIsValid_ShouldMoveProperly(Direction direction, int currentX, int currentY, int expectedX, int expectedY)
        {
            _moveValidatorMock.Setup(v => v.IsValidXMove(expectedX)).ReturnsAsync(true);
            _moveValidatorMock.Setup(v => v.IsValidYMove(expectedY)).ReturnsAsync(true);
            var position = new Point { X = currentX, Y = currentY };
            var expectedPosition = new Point { X = expectedX, Y = expectedY };
            var command = new MoveForwardCommand { CurrentPosition = position, Direction = direction };

            var result = await _moveCommandHandler.Handle(command, It.IsAny<CancellationToken>());

            Assert.Equal(expectedPosition, result.NewPosition);
        }

        [Fact]
        public async Task MoveForward_WhenXMoveIsNotValid_ShouldNotMove()
        {
            _moveValidatorMock.Setup(v => v.IsValidXMove(It.IsAny<int>())).ReturnsAsync(false);
            var direction = It.IsAny<Direction>();
            var position = It.IsAny<Point>();
            var command = new MoveForwardCommand { CurrentPosition = position, Direction = direction };

            var result = await _moveCommandHandler.Handle(command, It.IsAny<CancellationToken>());

            Assert.Equal(position, result.NewPosition);
        }

        [Fact]
        public async Task MoveForward_WhenYMoveIsNotValid_ShouldNotMove()
        {
            _moveValidatorMock.Setup(v => v.IsValidYMove(It.IsAny<int>())).ReturnsAsync(false);
            var direction = It.IsAny<Direction>();
            var position = It.IsAny<Point>();
            var command = new MoveForwardCommand { CurrentPosition = position, Direction = direction };

            var result = await _moveCommandHandler.Handle(command, It.IsAny<CancellationToken>());

            Assert.Equal(position, result.NewPosition);
        }
    }
}