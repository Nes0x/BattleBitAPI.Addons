using BattleBitAPI.Common;
using BattleBitAPI.Server;

namespace BattleBitAPI.Addons.EventHandler.Events.Handlers;

public abstract class OnPlayerJoinedASquadHandler<TPlayer> : EventHandler<TPlayer> where TPlayer : Player
{
    protected OnPlayerJoinedASquadHandler(ServerListener<TPlayer> serverListener) : base(serverListener)
    {
    }

    /// <summary>
    ///     Fired when a player joins a squad.
    /// </summary>
    /// <remarks>
    ///     TPlayer - The player<br />
    ///     Squads - The squad player joined<br />
    /// </remarks>
    protected abstract Task HandleAsync(TPlayer arg1, Squads arg2);

    public override void Subscribe()
    {
        ServerListener.OnPlayerJoinedASquad += HandleAsync;
    }

    public override void UnSubscribe()
    {
        ServerListener.OnPlayerJoinedASquad -= HandleAsync;
    }
}