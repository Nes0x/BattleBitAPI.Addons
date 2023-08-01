using BattleBitAPI.Common;
using BattleBitAPI.Server;

namespace BattleBitAPI.Addons.EventHandler.Events.Handlers;

public abstract class OnGetPlayerStatsHandler<TPlayer> : EventHandler<TPlayer> where TPlayer : Player
{
    protected OnGetPlayerStatsHandler(ServerListener<TPlayer> serverListener) : base(serverListener)
    {
    }

    /// <summary>
    ///     Fired when game server requests the stats of a player, this function should return in 3000ms or player will not
    ///     able to join to server.
    /// </summary>
    /// <remarks>
    ///     ulong - SteamID of the player<br />
    ///     PlayerStats - The official stats of the player<br />
    /// </remarks>
    /// <value>
    ///     Returns: The modified stats of the player.
    /// </value>
    protected abstract Task<PlayerStats> HandleAsync(ulong arg1, PlayerStats arg2);

    public override void Subscribe()
    {
        ServerListener.OnGetPlayerStats += HandleAsync;
    }

    public override void UnSubscribe()
    {
        ServerListener.OnGetPlayerStats -= HandleAsync;
    }
}