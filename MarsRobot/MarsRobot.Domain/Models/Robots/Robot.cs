using MarsRobot.Domain.Contracts.Robots;
using MarsRobot.Domain.Contracts.Strategies;
using MarsRobot.Domain.Contracts.ValueObjects;
using MarsRobot.Domain.Models.Enums;
using MarsRobot.Domain.Models.ValueObjects;
using MarsRobot.Domain.Strategies;
using MediatR;
using System.Threading.Tasks;

namespace MarsRobot.Domain.Models.Robots
{
    public class Robot : IRobot
    {
        private readonly IMediator _mediator;

        public Robot(IMediator mediator)
        {
            _mediator = mediator;
        }

        public IPoint Position { get; private set; } = Point.InitialPosition;
        public Direction Direction { get; private set; } = Direction.North;

        public async Task Navigate(string commands)
        {
            foreach (Command command in commands)
            {
                var movementStrategy = GetMovementStrategyForCommand(command);
                var result = await movementStrategy.PerformAction(Position, Direction);

                Position = result.Position;
                Direction = result.Direction;
            }
        }

        private IMovementStrategy GetMovementStrategyForCommand(Command command) =>
            command switch
            {
                Command.MoveForward => new MoveForwardStrategy(_mediator),
                Command.TurnLeft => new TurnLeftStrategy(_mediator),
                Command.TurnRight => new TurnRightStrategy(_mediator),
                _ => new NotImplementedStrategy()
            };
}
}