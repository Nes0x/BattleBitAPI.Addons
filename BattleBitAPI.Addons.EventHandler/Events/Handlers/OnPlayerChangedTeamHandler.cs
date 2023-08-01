using BattleBitAPI.Common;
using BattleBitAPI.Server;

namespace BattleBitAPI.Addons.EventHandler.Events.Handlers;

public abstract class OnPlayerChangedTeamHandler<TPlayer> : EventHandler<TPlayer> where TPlayer : Player
{
    protected OnPlayerChangedTeamHandler(ServerListener<TPlayer> serverListener) : base(serverListener)
    {
    }

    /// <summary>
    ///     Fired when a player changes team.
    /// </summary>
    /// <remarks>
    ///     TPlayer - The player<br />
    ///     Team - The new team that player joined<br />
    /// </remarks>
    protected abstract Task HandleAsync(TPlayer arg1, Team arg2);


    public override void Subscribe()
    {
        ServerListener.OnPlayerChangedTeam += HandleAsync;
    }

    public override void UnSubscribe()
    {
        ServerListener.OnPlayerChangedTeam -= HandleAsync;
    }
}