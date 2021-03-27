using MarsRobot.Domain.Commands.Turn;
using MarsRobot.Domain.Contracts.Strategies;
using MarsRobot.Domain.Contracts.ValueObjects;
using MarsRobot.Domain.Models.Enums;
using MediatR;
using System.Threading.Tasks;

namespace MarsRobot.Domain.Strategies
{
    public class TurnLeftStrategy : IMovementStrategy
    {
        private readonly IMediator _mediator;

        public TurnLeftStrategy(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<(IPoint Position, Direction Direction)> PerformAction(IPoint currentPosition, Direction currentDirection)
        {
            var command = new TurnLeftCommand { CurrentDirection = currentDirection };
            var turnResult = await _mediator.Send(command);

            return (Position: currentPosition, Direction: turnResult.NewDirection);
        }
    }
}
