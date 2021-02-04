using MarsRobot.Domain.Contracts.Validators;
using MarsRobot.Domain.Contracts.ValueObjects;
using MarsRobot.Domain.Models.Enums;
using MarsRobot.Domain.Models.ValueObjects;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace MarsRobot.Domain.Commands.Move
{
    public class MoveCommandHandler : IRequestHandler<MoveForwardCommand, MoveResult>
    {
        private readonly IMoveValidator _moveValidator;

        public MoveCommandHandler(IMoveValidator moveValidator)
        {
            _moveValidator = moveValidator;
        }

        public async Task<MoveResult> Handle(MoveForwardCommand request, CancellationToken cancellationToken)
        {
            int xAfterMoving = request.CurrentPosition.X;
            int yAfterMoving = request.CurrentPosition.Y;

            switch (request.Direction)
            {
                case Direction.North:
                    yAfterMoving++;
                    break;
                case Direction.East:
                    xAfterMoving++;
                    break;
                case Direction.South:
                    yAfterMoving--;
                    break;
                case Direction.West:
                    xAfterMoving--;
                    break;
            };

            if (!await _moveValidator.IsValidXMove(xAfterMoving) || !await _moveValidator.IsValidYMove(yAfterMoving))
                return CreateMoveResult(request.CurrentPosition);

            return CreateMoveResult(new Point { X = xAfterMoving, Y = yAfterMoving });
        }

        private MoveResult CreateMoveResult(IPoint position) => new MoveResult { NewPosition = position };
    }
}