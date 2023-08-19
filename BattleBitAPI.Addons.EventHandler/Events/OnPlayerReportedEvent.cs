using BattleBitAPI.Addons.Common;
using BattleBitAPI.Addons.EventHandler.Common;
using BattleBitAPI.Common;

namespace BattleBitAPI.Addons.EventHandler.Events;

public class OnPlayerReportedEvent : EventGameServer
{
    public OnPlayerReportedEvent(EventModule eventModule, Event @event) : base(eventModule, @event)
    {
    }

    public override Task OnPlayerReported(AddonPlayer from, AddonPlayer to, ReportReason reason, string additional)
    {
        return (Task)Event.MethodInfo.Invoke(EventModule, new[]
        {
            new OnPlayerReportedArgs()
            {
                From = from,
                To = to,
                ReportReason = reason,
                Additional = additional,
                GameServer = this
            }
        
        });
    }


}

public class OnPlayerReportedArgs : IGameServerArgs
{
    public required AddonPlayer From { get; init; }
    public required AddonPlayer To { get; init; }
    public required ReportReason ReportReason { get; init; }
    public required string Additional { get; init; }
    public required AddonGameServer GameServer { get; init; }
}