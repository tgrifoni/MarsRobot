using System.Threading.Tasks;

namespace MarsRobot.Domain.Contracts.Validators
{
    public interface IMoveYValidator
    {
        Task<bool> IsValidYMove(int yMove);
    }
}