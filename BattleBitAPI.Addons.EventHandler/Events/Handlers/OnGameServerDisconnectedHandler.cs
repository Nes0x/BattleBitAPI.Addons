using BattleBitAPI.Server;

namespace BattleBitAPI.Addons.EventHandler.Events.Handlers;

public abstract class OnGameServerDisconnectedHandler<TPlayer> : EventHandler<TPlayer> where TPlayer : Player
{
    protected OnGameServerDisconnectedHandler(ServerListener<TPlayer> serverListener) : base(serverListener)
    {
    }

    /// <summary>
    ///     Fired when a game server disconnects. Check (GameServer.TerminationReason) to see the reason.
    /// </summary>
    /// <remarks>
    ///     GameServer: Game server that disconnected.<br />
    /// </remarks>
    protected abstract Task HandleAsync(GameServer arg);

    public override void Subscribe()
    {
        ServerListener.OnGameServerDisconnected += HandleAsync;
    }

    public override void UnSubscribe()
    {
        ServerListener.OnGameServerDisconnected -= HandleAsync;
    }
}