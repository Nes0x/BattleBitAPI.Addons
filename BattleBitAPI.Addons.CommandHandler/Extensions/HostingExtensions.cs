using System.Reflection;
using BattleBitAPI.Addons.CommandHandler.Common;
using BattleBitAPI.Addons.CommandHandler.Handlers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BattleBitAPI.Addons.CommandHandler.Extensions;

public static class HostingExtensions
{
    public static IHostBuilder AddCommandHandler<TPlayer>(this IHostBuilder hostBuilder,
        CommandHandlerSettings commandHandlerSettings) where TPlayer : Player
    {
        var assembly = Assembly.GetEntryAssembly();

        if (assembly is null)
            throw new ArgumentNullException(
                nameof(assembly),
                "Unable to get assembly.");


        var types = assembly.GetTypes();
        var playerType = typeof(TPlayer);
        var targetType = typeof(Command<>).MakeGenericType(playerType);

        hostBuilder.ConfigureServices(services =>
        {
            foreach (var type in types)
                if (type.IsAssignableTo(targetType) && !type.IsAbstract)
                    services.AddSingleton(targetType, type);

            services.AddSingleton<CommandHandlerSettings>(_ => commandHandlerSettings);
            services.AddSingleton<MessageHandlerService<TPlayer>>();
            services.AddHostedService<CommandHandlerActivatorService<TPlayer>>();
        });

        return hostBuilder;
    }
}