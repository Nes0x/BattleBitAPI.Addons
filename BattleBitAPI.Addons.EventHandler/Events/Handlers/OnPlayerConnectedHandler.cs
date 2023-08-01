using BattleBitAPI.Server;

namespace BattleBitAPI.Addons.EventHandler.Events.Handlers;

public abstract class OnPlayerConnectedHandler<TPlayer> : EventHandler<TPlayer> where TPlayer : Player
{
    protected OnPlayerConnectedHandler(ServerListener<TPlayer> serverListener) : base(serverListener)
    {
    }

    /// <summary>
    ///     Fired when a player connects to a server.<br />
    ///     Check player.GameServer get the server that player joined.
    /// </summary>
    /// <remarks>
    ///     Player: The player that connected to the server<br />
    /// </remarks>
    protected abstract Task HandleAsync(TPlayer arg);

    public override void Subscribe()
    {
        ServerListener.OnPlayerConnected += HandleAsync;
    }

    public override void UnSubscribe()
    {
        ServerListener.OnPlayerConnected -= HandleAsync;
    }
}