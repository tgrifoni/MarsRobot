using MarsRobot.Domain.Contracts.ValueObjects;

namespace MarsRobot.Domain.Commands.Move
{
    public struct MoveResult
    {
        public IPoint NewPosition { get; init; }
    }
}