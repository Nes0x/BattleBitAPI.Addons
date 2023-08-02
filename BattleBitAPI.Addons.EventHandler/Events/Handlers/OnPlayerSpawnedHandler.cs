using BattleBitAPI.Server;

namespace BattleBitAPI.Addons.EventHandler.Events.Handlers;

public abstract class OnPlayerSpawnedHandler<TPlayer> : EventHandler<TPlayer> where TPlayer : Player
{
    protected OnPlayerSpawnedHandler(ServerListener<TPlayer> serverListener) : base(serverListener)
    {
    }

    /// <summary>
    ///     Fired when a player is spawns
    /// </summary>
    /// <remarks>
    ///     TPlayer - The player<br />
    /// </remarks>
    protected abstract Task HandleAsync(TPlayer arg);

    public override void Subscribe()
    {
        ServerListener.OnPlayerSpawned += HandleAsync;
    }

    public override void UnSubscribe()
    {
        ServerListener.OnPlayerSpawned -= HandleAsync;
    }
}