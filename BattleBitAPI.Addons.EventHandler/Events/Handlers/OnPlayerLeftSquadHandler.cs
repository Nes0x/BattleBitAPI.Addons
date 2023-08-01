using BattleBitAPI.Common;
using BattleBitAPI.Server;

namespace BattleBitAPI.Addons.EventHandler.Events.Handlers;

public abstract class OnPlayerLeftSquadHandler<TPlayer> : EventHandler<TPlayer> where TPlayer : Player
{
    protected OnPlayerLeftSquadHandler(ServerListener<TPlayer> serverListener) : base(serverListener)
    {
    }

    /// <summary>
    ///     Fired when a player leaves their squad.
    /// </summary>
    /// <remarks>
    ///     TPlayer - The player<br />
    ///     Squads - The squad that player left<br />
    /// </remarks>
    protected abstract Task HandleAsync(TPlayer arg1, Squads arg2);


    public override void Subscribe()
    {
        ServerListener.OnPlayerLeftSquad += HandleAsync;
    }

    public override void UnSubscribe()
    {
        ServerListener.OnPlayerLeftSquad -= HandleAsync;
    }
}