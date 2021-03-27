using MarsRobot.Domain.Commands.Move;
using MarsRobot.Domain.Contracts.Strategies;
using MarsRobot.Domain.Contracts.ValueObjects;
using MarsRobot.Domain.Models.Enums;
using MediatR;
using System.Threading.Tasks;

namespace MarsRobot.Domain.Strategies
{
    public class MoveForwardStrategy : IMovementStrategy
    {
        private readonly IMediator _mediator;

        public MoveForwardStrategy(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<(IPoint Position, Direction Direction)> PerformAction(IPoint currentPosition, Direction currentDirection)
        {
            var moveCommand = new MoveForwardCommand { CurrentPosition = currentPosition, Direction = currentDirection };
            var moveResult = await _mediator.Send(moveCommand);

            return (Position: moveResult.NewPosition, Direction: currentDirection);
        }
    }
}
