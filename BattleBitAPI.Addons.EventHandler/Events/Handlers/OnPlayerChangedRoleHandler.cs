using BattleBitAPI.Common;
using BattleBitAPI.Server;

namespace BattleBitAPI.Addons.EventHandler.Events.Handlers;

public abstract class OnPlayerChangedRoleHandler<TPlayer> : EventHandler<TPlayer> where TPlayer : Player
{
    protected OnPlayerChangedRoleHandler(ServerListener<TPlayer> serverListener) : base(serverListener)
    {
    }

    /// <summary>
    ///     Fired when a player changes their game role.
    /// </summary>
    /// <remarks>
    ///     TPlayer - The player<br />
    ///     GameRole - The new role of the player<br />
    /// </remarks>
    protected abstract Task HandleAsync(TPlayer arg1, GameRole arg2);

    public override void Subscribe()
    {
        ServerListener.OnPlayerChangedRole += HandleAsync;
    }

    public override void UnSubscribe()
    {
        ServerListener.OnPlayerChangedRole -= HandleAsync;
    }
}