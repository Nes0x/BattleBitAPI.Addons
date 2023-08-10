using BattleBitAPI.Addons.Common.Extensions;
using BattleBitAPI.Addons.EventHandler.Events;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BattleBitAPI.Addons.EventHandler.Extensions;

public static class HostBuilderExtensions
{
    /// <summary>
    /// Adds all event handlers from assembly to services and ServerListener
    /// </summary>
    /// <typeparam name="TPlayer">Your player type</typeparam>
    public static IHostBuilder AddEventHandler<TPlayer>(this IHostBuilder hostBuilder) where TPlayer : Player
    {
        var assembly = hostBuilder.GetAssembly();
        var types = assembly.GetTypes();
        var playerType = typeof(TPlayer);
        var targetType = typeof(IEventHandler<>).MakeGenericType(playerType);

        hostBuilder.ConfigureServices(services =>
        {
            foreach (var type in types)
                if (type.IsAssignableTo(targetType) && !type.IsAbstract)
                    services.AddSingleton(targetType, type);
            services.AddHostedService<EventHandlerActivatorService<TPlayer>>();
        });

        return hostBuilder;
    }
}