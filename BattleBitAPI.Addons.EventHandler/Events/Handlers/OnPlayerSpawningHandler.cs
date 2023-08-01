using BattleBitAPI.Common;
using BattleBitAPI.Server;

namespace BattleBitAPI.Addons.EventHandler.Events.Handlers;

public abstract class OnPlayerSpawningHandler<TPlayer> : EventHandler<TPlayer> where TPlayer : Player
{
    protected OnPlayerSpawningHandler(ServerListener<TPlayer> serverListener) : base(serverListener)
    {
    }

    /// <summary>
    ///     Fired when a player is spawning.
    /// </summary>
    /// <remarks>
    ///     TPlayer - The player<br />
    ///     PlayerSpawnRequest - The request<br />
    /// </remarks>
    /// <value>
    ///     Returns: The new spawn response
    /// </value>
    protected abstract Task<PlayerSpawnRequest> HandleAsync(TPlayer arg1, PlayerSpawnRequest arg2);


    public override void Subscribe()
    {
        ServerListener.OnPlayerSpawning += HandleAsync;
    }

    public override void UnSubscribe()
    {
        ServerListener.OnPlayerSpawning -= HandleAsync;
    }
}