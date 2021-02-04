using MarsRobot.Domain.Contracts.Validators;
using MarsRobot.Domain.Contracts.ValueObjects;
using MarsRobot.Domain.Models.ValueObjects;
using System.Threading.Tasks;

namespace MarsRobot.Domain.Validators
{
    public class MoveValidator : IMoveValidator
    {
        private static readonly IPoint _minimumLimit = Point.InitialPosition;

        private readonly IPoint _plateau;

        public MoveValidator(IPoint plateau)
        {
            _plateau = plateau;
        }

        public async Task<bool> IsValidXMove(int xMove) =>
            await Task.FromResult(xMove <= _plateau.X && xMove >= _minimumLimit.X);

        public async Task<bool> IsValidYMove(int yMove) =>
            await Task.FromResult(yMove <= _plateau.Y && yMove >= _minimumLimit.Y);
    }
}