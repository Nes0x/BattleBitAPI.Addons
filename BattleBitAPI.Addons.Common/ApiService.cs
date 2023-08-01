using System.Net;
using BattleBitAPI.Server;
using Microsoft.Extensions.Hosting;

namespace BattleBitAPI.Addons.Common;

public class ApiService<TPlayer> : IHostedService where TPlayer : Player
{
    private readonly IPAddress _ipAddress;
    private readonly int _port;
    private readonly ServerListener<TPlayer> _serverListener;

    public ApiService(ServerListener<TPlayer> serverListener, IPAddress ipAddress, int port)
    {
        _serverListener = serverListener;
        _ipAddress = ipAddress;
        _port = port;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        _serverListener.Start(_ipAddress, _port);
        await Task.CompletedTask;
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        _serverListener.Stop();
        await Task.CompletedTask;
    }
}