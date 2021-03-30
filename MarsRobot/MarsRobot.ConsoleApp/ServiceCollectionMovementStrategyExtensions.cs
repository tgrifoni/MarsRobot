using MarsRobot.Domain.Contracts.Strategies;
using MarsRobot.Domain.Models.Enums;
using MarsRobot.Domain.Strategies;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace MarsRobot.ConsoleApp
{
    public static class ServiceCollectionMovementStrategyExtensions
    {
        public static IServiceCollection AddStrategies(this IServiceCollection services) =>
            services
                .AddTransient<MoveForwardStrategy>()
                .AddTransient<TurnLeftStrategy>()
                .AddTransient<TurnRightStrategy>()
                .AddTransient<NotImplementedStrategy>()
                .AddTransient(GetMovementStrategy);

        private static Func<Command, IMovementStrategy> GetMovementStrategy(IServiceProvider serviceProvider) =>
            command => command switch
            {
                Command.MoveForward => serviceProvider.GetService<MoveForwardStrategy>(),
                Command.TurnLeft => serviceProvider.GetService<TurnLeftStrategy>(),
                Command.TurnRight => serviceProvider.GetService<TurnRightStrategy>(),
                _ => serviceProvider.GetService<NotImplementedStrategy>()
            };
    }
}
