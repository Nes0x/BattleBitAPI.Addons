using BattleBitAPI.Common;
using BattleBitAPI.Server;

namespace BattleBitAPI.Addons.EventHandler.Events.Handlers;

public abstract class OnPlayerRequestingToChangeRoleHandler<TPlayer> : EventHandler<TPlayer> where TPlayer : Player
{
    protected OnPlayerRequestingToChangeRoleHandler(ServerListener<TPlayer> serverListener) : base(serverListener)
    {
    }

    /// <summary>
    ///     Fired when a player requests server to change role.
    /// </summary>
    /// <remarks>
    ///     TPlayer - The player requesting<br />
    ///     GameRole - The role the player asking to change<br />
    /// </remarks>
    /// <value>
    ///     Returns: True if you accept if, false if you don't.
    /// </value>
    protected abstract Task<bool> HandleAsync(TPlayer arg1, GameRole arg2);

    public override void Subscribe()
    {
        ServerListener.OnPlayerRequestingToChangeRole += HandleAsync;
    }

    public override void UnSubscribe()
    {
        ServerListener.OnPlayerRequestingToChangeRole -= HandleAsync;
    }
}