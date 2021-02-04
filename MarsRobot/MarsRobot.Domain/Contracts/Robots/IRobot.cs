using MarsRobot.Domain.Contracts.ValueObjects;
using MarsRobot.Domain.Models.Enums;
using System.Threading.Tasks;

namespace MarsRobot.Domain.Contracts.Robots
{
    public interface IRobot
    {
        IPoint Position { get; }
        Direction Direction { get; }

        Task Navigate(string commands);
    }
}