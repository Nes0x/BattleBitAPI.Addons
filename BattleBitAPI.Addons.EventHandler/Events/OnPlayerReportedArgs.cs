using BattleBitAPI.Common;

namespace BattleBitAPI.Addons.EventHandler.Events;

public class OnPlayerReportedArgs<TPlayer> where TPlayer : Player
{
    public TPlayer Reporter { get; internal init; }
    public TPlayer Reported { get; internal init; }
    public ReportReason ReportReason { get; internal init; }
    public string Details { get; internal init; }
}