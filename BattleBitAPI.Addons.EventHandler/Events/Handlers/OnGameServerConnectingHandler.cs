using System.Net;
using BattleBitAPI.Server;

namespace BattleBitAPI.Addons.EventHandler.Events.Handlers;

public abstract class OnGameServerConnectingHandler<TPlayer> : EventHandler<TPlayer> where TPlayer : Player
{
    protected OnGameServerConnectingHandler(ServerListener<TPlayer> serverListener) : base(serverListener)
    {
    }

    /// <summary>
    ///     Fired when an attempt made to connect to the server.<br />
    ///     Default, any connection attempt will be accepted
    /// </summary>
    /// <remarks>
    ///     IPAddress: IP of incoming connection <br />
    /// </remarks>
    /// <value>
    ///     Returns: true if allow connection, false if deny the connection.
    /// </value>
    protected abstract Task<bool> HandleAsync(IPAddress arg);

    public override void Subscribe()
    {
        ServerListener.OnGameServerConnecting += HandleAsync;
    }

    public override void UnSubscribe()
    {
        ServerListener.OnGameServerConnecting -= HandleAsync;
    }
}