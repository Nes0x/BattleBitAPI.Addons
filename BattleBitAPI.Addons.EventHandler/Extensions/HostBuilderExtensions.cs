using BattleBitAPI.Addons.Common.Extensions;
using BattleBitAPI.Addons.EventHandler.Common;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BattleBitAPI.Addons.EventHandler.Extensions;

public static class HostBuilderExtensions
{
    /// <summary>
    ///     Adds all event handlers from assembly to services and ServerListener
    /// </summary>
    public static IHostBuilder AddEventHandler(this IHostBuilder hostBuilder)
    {
        var assembly = hostBuilder.GetAssembly();
        var types = assembly.GetTypes();
        var targetType = typeof(EventModule);

        hostBuilder.ConfigureServices(services =>
        {
            foreach (var type in types)
                if (type.IsAssignableTo(targetType) && !type.IsAbstract)
                    services.AddSingleton(targetType, type);
            services.AddHostedService<EventHandlerActivatorService>();
        });

        return hostBuilder;
    }
}