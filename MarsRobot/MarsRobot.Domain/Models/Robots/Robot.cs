using MarsRobot.Domain.Commands.Move;
using MarsRobot.Domain.Commands.Turn;
using MarsRobot.Domain.Contracts.Robots;
using MarsRobot.Domain.Contracts.ValueObjects;
using MarsRobot.Domain.Models.Enums;
using MarsRobot.Domain.Models.ValueObjects;
using MediatR;
using System.Threading.Tasks;

namespace MarsRobot.Domain.Models.Robots
{
    public class Robot : IRobot
    {
        private readonly IMediator _mediator;

        public IPoint Position { get; private set; } = Point.InitialPosition;
        public Direction Direction { get; private set; } = Direction.North;

        public Robot(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task Navigate(string commands)
        {
            foreach (Command command in commands)
            {
                switch (command)
                {
                    case Command.MoveForward:
                        var moveCommand = new MoveForwardCommand { CurrentPosition = Position, Direction = Direction };
                        var moveResult = await _mediator.Send(moveCommand);
                        Position = moveResult.NewPosition;
                        break;
                    case Command.TurnLeft:
                        var turnLeftCommand = new TurnLeftCommand { CurrentDirection = Direction };
                        var turnLeftResult = await _mediator.Send(turnLeftCommand);
                        Direction = turnLeftResult.NewDirection;
                        break;
                    case Command.TurnRight:
                        var turnRightCommand = new TurnRightCommand { CurrentDirection = Direction };
                        var turnRightResult = await _mediator.Send(turnRightCommand);
                        Direction = turnRightResult.NewDirection;
                        break;
                }
            }
        }
    }
}