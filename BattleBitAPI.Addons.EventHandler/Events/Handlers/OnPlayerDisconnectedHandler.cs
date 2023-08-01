using BattleBitAPI.Server;

namespace BattleBitAPI.Addons.EventHandler.Events.Handlers;

public abstract class OnPlayerDisconnectedHandler<TPlayer> : EventHandler<TPlayer> where TPlayer : Player
{
    protected OnPlayerDisconnectedHandler(ServerListener<TPlayer> serverListener) : base(serverListener)
    {
    }

    /// <summary>
    ///     Fired when a player disconnects from a server.<br />
    ///     Check player.GameServer get the server that player left.
    /// </summary>
    /// <remarks>
    ///     Player: The player that disconnected from the server<br />
    /// </remarks>
    protected abstract Task HandleAsync(TPlayer arg);

    public override void Subscribe()
    {
        ServerListener.OnPlayerDisconnected += HandleAsync;
    }

    public override void UnSubscribe()
    {
        ServerListener.OnPlayerDisconnected -= HandleAsync;
    }
}