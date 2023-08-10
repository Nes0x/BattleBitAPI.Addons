using BattleBitAPI.Addons.CommandHandler.Common;
using BattleBitAPI.Addons.CommandHandler.Converters;
using BattleBitAPI.Addons.CommandHandler.Converters.TypeReaders;
using BattleBitAPI.Addons.CommandHandler.Handlers;
using BattleBitAPI.Addons.CommandHandler.Modules;
using BattleBitAPI.Addons.CommandHandler.Validations;
using BattleBitAPI.Addons.Common.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BattleBitAPI.Addons.CommandHandler.Extensions;

public static class HostingExtensions
{
    /// <summary>
    /// Adds all commands from assembly to services and ServerListener
    /// </summary>
    /// <param name="commandHandlerSettings">Your settings for command handling</param>
    /// <typeparam name="TPlayer">Your player type</typeparam>
    public static IHostBuilder AddCommandHandler<TPlayer>(this IHostBuilder hostBuilder,
        CommandHandlerSettings commandHandlerSettings) where TPlayer : Player
    {
        var assembly = hostBuilder.GetAssembly();
        var types = assembly.GetTypes();
        var targetType = typeof(CommandModule<TPlayer>);

        hostBuilder.ConfigureServices(services =>
        {
            foreach (var type in types)
                if (type.IsAssignableTo(targetType) && !type.IsAbstract)
                {
                    services.AddSingleton(targetType, type);
                }

            if (commandHandlerSettings.DefaultHelpCommand) services.AddSingleton(targetType, typeof(HelpModule<TPlayer>));
            services.AddSingleton<CommandHandlerSettings>(_ => commandHandlerSettings);
            services.AddSingleton<IMessageHandler<TPlayer>, MessageHandlerService<TPlayer>>();
            services.AddSingleton<IValidator<TPlayer>, CommandValidator<TPlayer>>();
            services.AddSingleton<IConverter<TPlayer>, CommandConverter<TPlayer>>();
            services.AddHostedService<CommandHandlerActivatorService<TPlayer>>();
        });

        return hostBuilder;
    }

    /// <summary>
    /// Adds all TypeReaders from assemblies to services
    /// </summary>
    /// <typeparam name="TPlayer">Your player type</typeparam>
    public static IHostBuilder AddTypeReaders<TPlayer>(this IHostBuilder hostBuilder) where TPlayer : Player
    {
        var assemblies = hostBuilder.GetAssemblies();
        var targetType = typeof(TypeReader<TPlayer>);
        foreach (var assembly in assemblies)
        {
            var types = assembly.GetTypes();
            hostBuilder.ConfigureServices(services =>
            {
                foreach (var type in types)
                    if (type.IsAssignableTo(targetType) && !type.IsAbstract)
                        services.AddSingleton(targetType, type);
            });
        }

        return hostBuilder;
    }
}