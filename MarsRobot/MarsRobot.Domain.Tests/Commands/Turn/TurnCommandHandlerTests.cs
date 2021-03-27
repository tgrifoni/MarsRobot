using MarsRobot.Domain.Commands.Turn;
using MarsRobot.Domain.Models.Enums;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace MarsRobot.Domain.Tests.Commands
{
    public class TurnCommandHandlerTests
    {
        private readonly TurnCommandHandler _turnCommandHandler = new();

        [Theory]
        [InlineData(Direction.North, Direction.East)]
        [InlineData(Direction.East, Direction.South)]
        [InlineData(Direction.South, Direction.West)]
        [InlineData(Direction.West, Direction.North)]
        public async Task TurnRight_WhenCalled_ShouldReturnProperDirection(Direction currentDirection, Direction expectedDirection)
        {
            var command = new TurnRightCommand { CurrentDirection = currentDirection };

            var result = await _turnCommandHandler.Handle(command, It.IsAny<CancellationToken>());

            Assert.Equal(expectedDirection, result.NewDirection);
        }

        [Theory]
        [InlineData(Direction.North, Direction.West)]
        [InlineData(Direction.West, Direction.South)]
        [InlineData(Direction.South, Direction.East)]
        [InlineData(Direction.East, Direction.North)]
        public async Task TurnLeft_WhenCalled_ShouldReturnProperDirection(Direction currentDirection, Direction expectedDirection)
        {
            var command = new TurnLeftCommand { CurrentDirection = currentDirection };

            var result = await _turnCommandHandler.Handle(command, It.IsAny<CancellationToken>());

            Assert.Equal(expectedDirection, result.NewDirection);
        }
    }
}