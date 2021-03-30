using MarsRobot.Domain.Contracts.Robots;
using MarsRobot.Domain.Contracts.Strategies;
using MarsRobot.Domain.Contracts.ValueObjects;
using MarsRobot.Domain.Models.Enums;
using MarsRobot.Domain.Models.Robots;
using MarsRobot.Domain.Models.ValueObjects;
using MarsRobot.Domain.Strategies;
using MediatR;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace MarsRobot.Domain.Tests.Models.Robots
{
    public class RobotTests
    {
        private readonly Mock<Func<Command, IMovementStrategy>> _strategyResolverMock = new();
        private readonly IPoint _initialPosition;
        private readonly Direction _initialDirection;
        private readonly IRobot _robot;

        public RobotTests()
        {
            _robot = new Robot(_strategyResolverMock.Object);
            _initialPosition = _robot.Position;
            _initialDirection = _robot.Direction;
        }

        [Fact]
        public async Task Navigate_WhenThereIsAMoveForwardCommand_ShouldUseMoveForwardStrategy()
        {
            var mediator = Mock.Of<IMediator>();
            var strategyMock = new Mock<MoveForwardStrategy>(mediator).As<IMovementStrategy>();
            var expectedPosition = new Point { X = 1, Y = 2 };
            var expectedResult = (Position: expectedPosition, _robot.Direction);
            strategyMock.Setup(s => s.PerformAction(_robot.Position, _robot.Direction)).ReturnsAsync(expectedResult);
            _strategyResolverMock.Setup(sr => sr(Command.MoveForward)).Returns(strategyMock.Object);
            var commands = "F";

            await _robot.Navigate(commands);

            _strategyResolverMock.Verify(sr => sr(Command.MoveForward), Times.Once);
            strategyMock.Verify(s => s.PerformAction(_initialPosition, _initialDirection), Times.Once);
            Assert.IsAssignableFrom<MoveForwardStrategy>(strategyMock.Object);
            Assert.Equal(expectedResult.Position, _robot.Position);
            Assert.Equal(expectedResult.Direction, _robot.Direction);
        }

        [Fact]
        public async Task Navigate_WhenThereIsATurnRightCommand_ShouldUseTurnRightStrategy()
        {
            var mediator = Mock.Of<IMediator>();
            var strategyMock = new Mock<TurnRightStrategy>(mediator).As<IMovementStrategy>();
            var expectedResult = (_robot.Position, Direction: Direction.East);
            strategyMock.Setup(s => s.PerformAction(_robot.Position, _robot.Direction)).ReturnsAsync(expectedResult);
            _strategyResolverMock.Setup(sr => sr(Command.TurnRight)).Returns(strategyMock.Object);
            var commands = "R";

            await _robot.Navigate(commands);

            _strategyResolverMock.Verify(sr => sr(Command.TurnRight), Times.Once);
            strategyMock.Verify(s => s.PerformAction(_initialPosition, _initialDirection), Times.Once);
            Assert.IsAssignableFrom<TurnRightStrategy>(strategyMock.Object);
            Assert.Equal(expectedResult.Position, _robot.Position);
            Assert.Equal(expectedResult.Direction, _robot.Direction);
        }

        [Fact]
        public async Task Navigate_WhenThereIsATurnLeftCommand_ShouldUseTurnLeftStrategy()
        {
            var mediator = Mock.Of<IMediator>();
            var strategyMock = new Mock<TurnLeftStrategy>(mediator).As<IMovementStrategy>();
            var expectedResult = (_robot.Position, Direction: Direction.West);
            strategyMock.Setup(s => s.PerformAction(_robot.Position, _robot.Direction)).ReturnsAsync(expectedResult);
            _strategyResolverMock.Setup(sr => sr(Command.TurnLeft)).Returns(strategyMock.Object);
            var commands = "L";

            await _robot.Navigate(commands);

            _strategyResolverMock.Verify(sr => sr(Command.TurnLeft), Times.Once);
            strategyMock.Verify(s => s.PerformAction(_initialPosition, _initialDirection), Times.Once);
            Assert.IsAssignableFrom<TurnLeftStrategy>(strategyMock.Object);
            Assert.Equal(expectedResult.Position, _robot.Position);
            Assert.Equal(expectedResult.Direction, _robot.Direction);
        }

        [Fact]
        public async Task Navigate_WhenThereIsAnUnknownCommand_ShouldUseNotImplementedStrategy()
        {
            var strategyMock = new Mock<NotImplementedStrategy>().As<IMovementStrategy>();
            var expectedResult = (_robot.Position, _robot.Direction);
            strategyMock.Setup(s => s.PerformAction(_robot.Position, _robot.Direction)).ReturnsAsync(expectedResult);
            var unknownCommandChar = 'A';
            var unknownCommand = (Command)unknownCommandChar;
            _strategyResolverMock.Setup(sr => sr(unknownCommand)).Returns(strategyMock.Object);

            await _robot.Navigate(unknownCommandChar.ToString());

            _strategyResolverMock.Verify(sr => sr(unknownCommand), Times.Once);
            strategyMock.Verify(s => s.PerformAction(_initialPosition, _initialDirection), Times.Once);
            Assert.IsAssignableFrom<NotImplementedStrategy>(strategyMock.Object);
            Assert.Equal(expectedResult.Position, _robot.Position);
            Assert.Equal(expectedResult.Direction, _robot.Direction);
        }
    }
}