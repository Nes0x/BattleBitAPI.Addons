using BattleBitAPI.Server;

namespace BattleBitAPI.Addons.EventHandler.Events.Handlers;

public abstract class OnGameServerConnectedHandler<TPlayer> : EventHandler<TPlayer> where TPlayer : Player
{
    protected OnGameServerConnectedHandler(ServerListener<TPlayer> serverListener) : base(serverListener)
    {
    }

    /// <summary>
    ///     Fired when a game server connects.
    /// </summary>
    /// <remarks>
    ///     GameServer: Game server that is connecting.<br />
    /// </remarks>
    protected abstract Task HandleAsync(GameServer arg);

    public override void Subscribe()
    {
        ServerListener.OnGameServerConnected += HandleAsync;
    }

    public override void UnSubscribe()
    {
        ServerListener.OnGameServerConnected -= HandleAsync;
    }
}