using MarsRobot.Domain.Models.Enums;
using MediatR;

namespace MarsRobot.Domain.Commands.Turn
{
    public struct TurnRightCommand : IRequest<TurnResult>
    {
        public Direction CurrentDirection { get; init; }
    }
}