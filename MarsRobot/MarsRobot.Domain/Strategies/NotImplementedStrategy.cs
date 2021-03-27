using MarsRobot.Domain.Contracts.Strategies;
using MarsRobot.Domain.Contracts.ValueObjects;
using MarsRobot.Domain.Models.Enums;
using System.Threading.Tasks;

namespace MarsRobot.Domain.Strategies
{
    public class NotImplementedStrategy : IMovementStrategy
    {
        public Task<(IPoint Position, Direction Direction)> PerformAction(IPoint currentPosition, Direction currentDirection) =>
            Task.FromResult((Position: currentPosition, Direction: currentDirection));
    }
}
