using BattleBitAPI.Server;

namespace BattleBitAPI.Addons.EventHandler.Events.Handlers;

public abstract class OnGameServerTickHandler<TPlayer> : EventHandler<TPlayer> where TPlayer : Player
{
    protected OnGameServerTickHandler(ServerListener<TPlayer> serverListener) : base(serverListener)
    {
    }

    /// <summary>
    ///     Fired when game server is ticking (~100hz)<br />
    /// </summary>
    protected abstract Task HandleAsync(GameServer arg);

    public override void Subscribe()
    {
        ServerListener.OnGameServerTick += HandleAsync;
    }

    public override void UnSubscribe()
    {
        ServerListener.OnGameServerTick -= HandleAsync;
    }
}