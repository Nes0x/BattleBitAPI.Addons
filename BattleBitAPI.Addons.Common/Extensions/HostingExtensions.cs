using System.Net;
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
}