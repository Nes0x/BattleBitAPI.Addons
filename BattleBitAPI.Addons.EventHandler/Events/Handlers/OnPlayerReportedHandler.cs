using BattleBitAPI.Common;
using BattleBitAPI.Server;

namespace BattleBitAPI.Addons.EventHandler.Events.Handlers;

public abstract class OnPlayerReportedHandler<TPlayer> : EventHandler<TPlayer> where TPlayer : Player
{
    protected OnPlayerReportedHandler(ServerListener<TPlayer> serverListener) : base(serverListener)
    {
    }

    /// <summary>
    ///     Fired when a player reports another player.
    /// </summary>
    /// <remarks>
    ///     TPlayer - The reporter player<br />
    ///     TPlayer - The reported player<br />
    ///     ReportReason - The reason of report<br />
    ///     String - Additional detail<br />
    /// </remarks>
    protected abstract Task HandleAsync(TPlayer arg1, TPlayer arg2, ReportReason arg3, string arg4);

    public override void Subscribe()
    {
        ServerListener.OnPlayerReported += HandleAsync;
    }

    public override void UnSubscribe()
    {
        ServerListener.OnPlayerReported -= HandleAsync;
    }
}