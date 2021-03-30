using MarsRobot.Domain.Contracts.Robots;
using MarsRobot.Domain.Contracts.Strategies;
using MarsRobot.Domain.Contracts.ValueObjects;
using MarsRobot.Domain.Models.Enums;
using MarsRobot.Domain.Models.ValueObjects;
using System;
using System.Threading.Tasks;

namespace MarsRobot.Domain.Models.Robots
{
    public class Robot : IRobot
    {
        private readonly Func<Command, IMovementStrategy> _strategyResolver;

        public Robot(Func<Command, IMovementStrategy> strategyResolver)
        {
            _strategyResolver = strategyResolver;
        }

        public IPoint Position { get; private set; } = Point.InitialPosition;
        public Direction Direction { get; private set; } = Direction.North;

        public async Task Navigate(string commands)
        {
            foreach (Command command in commands)
            {
                var movementStrategy = _strategyResolver(command);
                var result = await movementStrategy.PerformAction(Position, Direction);

                Position = result.Position;
                Direction = result.Direction;
            }
        }
    }
}