using MarsRobot.Domain.Models.Enums;

namespace MarsRobot.Domain.Commands.Turn
{
    public struct TurnResult
    {
        public Direction NewDirection { get; init; }
    }
}