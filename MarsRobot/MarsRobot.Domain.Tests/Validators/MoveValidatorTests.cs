using MarsRobot.Domain.Contracts.Validators;
using MarsRobot.Domain.Contracts.ValueObjects;
using MarsRobot.Domain.Validators;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace MarsRobot.Domain.Tests.Validators
{
    public class MoveValidatorTests
    {
        private readonly Mock<IPoint> _plateauMock = new Mock<IPoint>();
        private readonly IMoveValidator _moveValidator;

        public MoveValidatorTests()
        {
            _moveValidator = new MoveValidator(_plateauMock.Object);
        }

        [Theory]
        [InlineData(2, 1)]
        [InlineData(2, 2)]
        public async Task IsValidXMove_WhenMoveIsValid_ShouldReturnTrue(int plateauX, int xMove)
        {
            _plateauMock.SetupGet(p => p.X).Returns(plateauX);

            var isValid = await _moveValidator.IsValidXMove(xMove);

            Assert.True(isValid);
        }

        [Fact]
        public async Task IsValidXMove_WhenMoveExceedsLowerBoundary_ShouldReturnFalse()
        {
            var plateauX = 2;
            _plateauMock.SetupGet(p => p.X).Returns(plateauX);
            var xMove = 0;

            var isValid = await _moveValidator.IsValidXMove(xMove);

            Assert.False(isValid);
        }

        [Fact]
        public async Task IsValidXMove_WhenMoveExceedsUpperBoundary_ShouldReturnFalse()
        {
            var plateauX = 2;
            _plateauMock.SetupGet(p => p.X).Returns(plateauX);
            var xMove = 3;

            var isValid = await _moveValidator.IsValidXMove(xMove);

            Assert.False(isValid);
        }

        [Theory]
        [InlineData(2, 1)]
        [InlineData(2, 2)]
        public async Task IsValidYMove_WhenMoveIsValid_ShouldReturnTrue(int plateauY, int yMove)
        {
            _plateauMock.SetupGet(p => p.Y).Returns(plateauY);

            var isValid = await _moveValidator.IsValidYMove(yMove);

            Assert.True(isValid);
        }

        [Fact]
        public async Task IsValidYMove_WhenMoveExceedsLowerBoundary_ShouldReturnFalse()
        {
            var plateauY = 2;
            _plateauMock.SetupGet(p => p.Y).Returns(plateauY);
            var yMove = 0;

            var isValid = await _moveValidator.IsValidYMove(yMove);

            Assert.False(isValid);
        }

        [Fact]
        public async Task IsValidYMove_WhenMoveExceedsUpperBoundary_ShouldReturnFalse()
        {
            var plateauY = 2;
            _plateauMock.SetupGet(p => p.Y).Returns(plateauY);
            var yMove = 3;

            var isValid = await _moveValidator.IsValidYMove(yMove);

            Assert.False(isValid);
        }
    }
}