using MarsRobot.Domain.Contracts.ValueObjects;

namespace MarsRobot.Domain.Models.ValueObjects
{
    public struct Point : IPoint
    {
        public int X { get; init; }
        public int Y { get; init; }

        public static Point InitialPosition => new Point { X = 1, Y = 1 };
    }
}