using MarsRobot.Domain.Contracts.Robots;
using MarsRobot.Domain.Models.ValueObjects;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MarsRobot.ConsoleApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var dimensionSeparator = "x";
            var plateauDimensions = Console.ReadLine().Split(dimensionSeparator).Select(value => Convert.ToInt32(value));
            var plateauLength = plateauDimensions.First();
            var plateauHeight = plateauDimensions.Last();
            var plateau = new Point { X = plateauLength, Y = plateauHeight };
            var commands = Console.ReadLine();

            using var host = HostBuilderFactory.CreateHostBuilder(args, plateau).Build();

            var robot = host.Services.GetRequiredService<IRobot>();
            await robot.Navigate(commands);
            Console.WriteLine($"{robot.Position.X}, {robot.Position.Y}, {robot.Direction}");

            await host.StopAsync();
        }
    }
}