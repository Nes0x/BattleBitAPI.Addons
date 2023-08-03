using System.Net;
using System.Reflection;
using BattleBitAPI.Server;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;

namespace BattleBitAPI.Addons.Common.Extensions;

public static class HostingExtensions
{
    public static IHostBuilder AddServerListener<TPlayer>(this IHostBuilder hostBuilder, int port,
        IPAddress ipAddress = null) where TPlayer : Player
    {
        ipAddress ??= IPAddress.Loopback;
        hostBuilder.ConfigureServices(services =>
        {
            services.TryAddSingleton<ServerListener<TPlayer>>();
            services.AddHostedService<ApiService<TPlayer>>(provider =>
            {
                var serverListener = provider.GetRequiredService<ServerListener<TPlayer>>();
                return new ApiService<TPlayer>(serverListener, ipAddress, port);
            });
        });
        return hostBuilder;
    }

    public static Assembly GetAssembly(this IHostBuilder hostBuilder)
    {
        var assembly = Assembly.GetEntryAssembly();
        if (assembly is null)
            throw new ArgumentNullException(
                nameof(assembly),
                "Unable to get assembly.");
        return assembly;
    }
}