using BattleBitAPI.Addons.Common;
using BattleBitAPI.Addons.EventHandler.Common;
using BattleBitAPI.Common;

namespace BattleBitAPI.Addons.EventHandler.Events;

public class OnAPlayerDownedAnotherPlayerEvent : EventGameServer
{
    public OnAPlayerDownedAnotherPlayerEvent(EventModule eventModule, Event @event) : base(eventModule, @event)
    {
    }

    public override Task OnAPlayerDownedAnotherPlayer(OnPlayerKillArguments<AddonPlayer> args)
    {
        return (Task)Event.MethodInfo.Invoke(EventModule, new[]
        {
            new OnAPlayerDownedAnotherPlayerArgs()
            {
                PlayerKillArguments = args,
                GameServer = this
            }
        });
    }
    
}

public class OnAPlayerDownedAnotherPlayerArgs : IGameServerArgs
{
    public required OnPlayerKillArguments<AddonPlayer> PlayerKillArguments { get; init; }
    public required AddonGameServer GameServer { get; init; }
}