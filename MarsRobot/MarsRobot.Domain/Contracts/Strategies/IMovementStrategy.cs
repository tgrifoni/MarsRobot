using MarsRobot.Domain.Contracts.ValueObjects;
using MarsRobot.Domain.Models.Enums;
using System.Threading.Tasks;

namespace MarsRobot.Domain.Contracts.Strategies
{
    public interface IMovementStrategy
    {
        Task<(IPoint Position, Direction Direction)> PerformAction(IPoint currentPosition, Direction currentDirection);
    }
}
