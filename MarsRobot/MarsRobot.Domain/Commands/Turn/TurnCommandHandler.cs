using MarsRobot.Domain.Models.Enums;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace MarsRobot.Domain.Commands.Turn
{
    public class TurnCommandHandler : IRequestHandler<TurnRightCommand, TurnResult>, IRequestHandler<TurnLeftCommand, TurnResult>
    {
        public async Task<TurnResult> Handle(TurnRightCommand request, CancellationToken cancellationToken) =>
            await Task.FromResult(new TurnResult
            {
                NewDirection = request.CurrentDirection switch
                {
                    Direction.North => Direction.East,
                    Direction.East => Direction.South,
                    Direction.South => Direction.West,
                    _ => Direction.North
                }
            });

        public async Task<TurnResult> Handle(TurnLeftCommand request, CancellationToken cancellationToken) =>
            await Task.FromResult(new TurnResult
            {
                NewDirection = request.CurrentDirection switch
                {
                    Direction.North => Direction.West,
                    Direction.West => Direction.South,
                    Direction.South => Direction.East,
                    _ => Direction.North
                }
            });
    }
}