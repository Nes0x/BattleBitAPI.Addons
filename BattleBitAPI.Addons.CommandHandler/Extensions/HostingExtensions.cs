using BattleBitAPI.Addons.CommandHandler.Common;
using BattleBitAPI.Addons.CommandHandler.Converters;
using BattleBitAPI.Addons.CommandHandler.Converters.TypeReaders;
using BattleBitAPI.Addons.CommandHandler.Handlers;
using BattleBitAPI.Addons.CommandHandler.Validations;
using BattleBitAPI.Addons.Common.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BattleBitAPI.Addons.CommandHandler.Extensions;

public static class HostingExtensions
{
    public static IHostBuilder AddCommandHandler<TPlayer>(this IHostBuilder hostBuilder,
        CommandHandlerSettings commandHandlerSettings) where TPlayer : Player
    {
        var assembly = hostBuilder.GetAssembly();
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
            services.AddSingleton<CommandValidator<TPlayer>>();
            services.AddSingleton<CommandConverter<TPlayer>>();
            services.AddHostedService<CommandHandlerActivatorService<TPlayer>>();
        });

        return hostBuilder;
    }

    public static IHostBuilder AddTypeReaders<TPlayer>(this IHostBuilder hostBuilder) where TPlayer : Player
    {
        var assemblies = hostBuilder.GetAssemblies();
        var playerType = typeof(TPlayer);
        var targetType = typeof(TypeReader<>).MakeGenericType(playerType);
        foreach (var assembly in assemblies)
        {
            var types = assembly.GetTypes();
            hostBuilder.ConfigureServices(services =>
            {
                foreach (var type in types)
                    if (type.IsAssignableTo(targetType) && !type.IsAbstract)
                    {
                        Console.WriteLine(type.Name);
                        services.AddSingleton(targetType, type);
                    }
            });
        }
        return hostBuilder;
    }
}