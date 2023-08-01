using BattleBitAPI.Server;

namespace BattleBitAPI.Addons.EventHandler.Events.Handlers;

public abstract class OnGameServerReconnectedHandler<TPlayer> : EventHandler<TPlayer> where TPlayer : Player
{
    protected OnGameServerReconnectedHandler(ServerListener<TPlayer> serverListener) : base(serverListener)
    {
    }

    /// <summary>
    ///     Fired when a game server reconnects. (When game server connects while a socket is already open)
    /// </summary>
    /// <remarks>
    ///     GameServer: Game server that is reconnecting.<br />
    /// </remarks>
    protected abstract Task HandleAsync(GameServer arg);

    public override void Subscribe()
    {
        ServerListener.OnGameServerReconnected += HandleAsync;
    }

    public override void UnSubscribe()
    {
        ServerListener.OnGameServerReconnected -= HandleAsync;
    }
}