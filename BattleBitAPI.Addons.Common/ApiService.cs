using System.Net;
using BattleBitAPI.Server;
using Microsoft.Extensions.Hosting;

namespace BattleBitAPI.Addons.Common;

public class ApiService : IHostedService
{
    private readonly IPAddress _ipAddress;
    private readonly int _port;
    private readonly ServerListener<AddonPlayer, AddonGameServer> _serverListener;

    public ApiService(ServerListener<AddonPlayer, AddonGameServer> serverListener, IPAddress ipAddress, int port)
    {
        _serverListener = serverListener;
        _ipAddress = ipAddress;
        _port = port;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _serverListener.Start(_ipAddress, _port);
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _serverListener.Stop();
        return Task.CompletedTask;
    }
}