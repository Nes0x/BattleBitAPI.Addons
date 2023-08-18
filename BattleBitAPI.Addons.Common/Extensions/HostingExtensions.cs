using System.Net;
using System.Reflection;
using BattleBitAPI.Server;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;

namespace BattleBitAPI.Addons.Common.Extensions;

public static class HostingExtensions
{
    /// <summary>
    ///     Try adds ServerListener to services
    /// </summary>
    /// <param name="port">Port on which the server is running</param>
    /// <param name="ipAddress">Ip address of server</param>
    /// <typeparam name="TPlayer">Your player type</typeparam>
    public static IHostBuilder AddServerListener(this IHostBuilder hostBuilder, int port,
        IPAddress ipAddress = null)
    {
        ipAddress ??= IPAddress.Loopback;
        hostBuilder.ConfigureServices(services =>
        {
            services.TryAddSingleton<ServerListener<AddonPlayer, AddonGameServer>>();
            services.AddHostedService<ApiService>(provider =>
            {
                var serverListener = provider.GetRequiredService<ServerListener<AddonPlayer, AddonGameServer>>();
                return new ApiService(serverListener, ipAddress, port);
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

    public static Assembly[] GetAssemblies(this IHostBuilder hostBuilder)
    {
        var assemblies = AppDomain.CurrentDomain.GetAssemblies();
        if (assemblies.Length == 0)
            throw new ArgumentNullException(
                nameof(assemblies),
                "Unable to get assemblies.");
        return assemblies;
    }
}