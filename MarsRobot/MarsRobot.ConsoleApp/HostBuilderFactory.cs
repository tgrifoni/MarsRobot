using MarsRobot.Domain.Commands.Move;
using MarsRobot.Domain.Contracts.Robots;
using MarsRobot.Domain.Contracts.Validators;
using MarsRobot.Domain.Contracts.ValueObjects;
using MarsRobot.Domain.Models.Robots;
using MarsRobot.Domain.Validators;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace MarsRobot.ConsoleApp
{
    public class HostBuilderFactory
    {
        public static IHostBuilder CreateHostBuilder(string[] args, IPoint plateau) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices(services => services
                    .AddTransient<IRobot, Robot>()
                    .AddTransient<IMoveValidator, MoveValidator>(provider => ActivatorUtilities.CreateInstance<MoveValidator>(provider, plateau))
                    .AddStrategies()
                    .AddMediatR(typeof(MoveForwardCommand)));
    }
}
