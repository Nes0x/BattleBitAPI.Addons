using BattleBitAPI.Common;
using BattleBitAPI.Server;

namespace BattleBitAPI.Addons.EventHandler.Events.Handlers;

public abstract class OnSavePlayerStatsHandler<TPlayer> : EventHandler<TPlayer> where TPlayer : Player
{
    protected OnSavePlayerStatsHandler(ServerListener<TPlayer> serverListener) : base(serverListener)
    {
    }

    /// <summary>
    ///     Fired when game server requests to save the stats of a player.
    /// </summary>
    /// <remarks>
    ///     ulong - SteamID of the player<br />
    ///     PlayerStats - Stats of the player<br />
    /// </remarks>
    /// <value>
    ///     Returns: The stats of the player.
    /// </value>
    protected abstract Task HandleAsync(ulong arg1, PlayerStats arg2);

    public override void Subscribe()
    {
        ServerListener.OnSavePlayerStats += HandleAsync;
    }

    public override void UnSubscribe()
    {
        ServerListener.OnSavePlayerStats -= HandleAsync;
    }
}