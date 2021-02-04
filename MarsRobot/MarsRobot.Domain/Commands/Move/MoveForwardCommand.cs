using MarsRobot.Domain.Contracts.ValueObjects;
using MarsRobot.Domain.Models.Enums;
using MediatR;

namespace MarsRobot.Domain.Commands.Move
{
    public struct MoveForwardCommand : IRequest<MoveResult>
    {
        public IPoint CurrentPosition { get; init; }
        public Direction Direction { get; init; }
    }
}