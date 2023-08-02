using BattleBitAPI.Server;

namespace BattleBitAPI.Addons.EventHandler.Events.Handlers;

public abstract class OnPlayerDiedHandler<TPlayer> : EventHandler<TPlayer> where TPlayer : Player
{
    protected OnPlayerDiedHandler(ServerListener<TPlayer> serverListener) : base(serverListener)
    {
    }

    /// <summary>
    ///     Fired when a player dies
    /// </summary>
    /// <remarks>
    ///     TPlayer - The player<br />
    /// </remarks>
    protected abstract Task HandleAsync(TPlayer arg);

    public override void Subscribe()
    {
        ServerListener.OnPlayerDied += HandleAsync;
    }

    public override void UnSubscribe()
    {
        ServerListener.OnPlayerDied -= HandleAsync;
    }
}