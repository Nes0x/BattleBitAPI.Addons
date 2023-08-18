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
    ///     Adds all commands from assembly to services and ServerListener
    /// </summary>
    /// <param name="commandHandlerSettings">Your settings for command handling</param>
    public static IHostBuilder AddCommandHandler(this IHostBuilder hostBuilder,
        CommandHandlerSettings commandHandlerSettings)
    {
        var assembly = hostBuilder.GetAssembly();
        var types = assembly.GetTypes();
        var targetType = typeof(CommandModule);

        hostBuilder.ConfigureServices(services =>
        {
            foreach (var type in types)
                if (type.IsAssignableTo(targetType) && !type.IsAbstract)
                    services.AddSingleton(targetType, type);

            if (commandHandlerSettings.DefaultHelpCommand)
                services.AddSingleton(targetType, typeof(HelpModule));
            services.AddSingleton<CommandHandlerSettings>(_ => commandHandlerSettings);
            services.AddSingleton<IValidator, CommandValidator>();
            services.AddSingleton<IConverter, CommandConverter>();
            services.AddHostedService<CommandHandlerActivatorService>();
        });

        return hostBuilder;
    }

    /// <summary>
    ///     Adds all TypeReaders from assemblies to services
    /// </summary>
    public static IHostBuilder AddTypeReaders(this IHostBuilder hostBuilder)
    {
        var assemblies = hostBuilder.GetAssemblies();
        var targetType = typeof(TypeReader);
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