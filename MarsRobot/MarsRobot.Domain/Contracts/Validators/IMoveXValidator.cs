using System.Threading.Tasks;

namespace MarsRobot.Domain.Contracts.Validators
{
    public interface IMoveXValidator
    {
        Task<bool> IsValidXMove(int xMove);
    }
}