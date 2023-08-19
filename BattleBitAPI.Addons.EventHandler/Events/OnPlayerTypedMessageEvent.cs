using BattleBitAPI.Addons.Common;
using BattleBitAPI.Addons.EventHandler.Common;
using BattleBitAPI.Common;

namespace BattleBitAPI.Addons.EventHandler.Events;

public class OnPlayerTypedMessageEvent : EventGameServer
{
    public OnPlayerTypedMessageEvent(EventModule eventModule, Event @event) : base(eventModule, @event)
    {
    }

    public override Task<bool> OnPlayerTypedMessage(AddonPlayer player, ChatChannel chatChannel, string message)
    {
        return (Task<bool>)Event.MethodInfo.Invoke(EventModule, new[]
        {
            new OnPlayerTypedMessageArgs
            {
                Player = player,
                ChatChannel = chatChannel,
                Message = message,
                GameServer = this
            }
        });
    }
}

public class OnPlayerTypedMessageArgs : IPlayerArgs, IGameServerArgs
{
    public required ChatChannel ChatChannel { get; init; }
    public required string Message { get; init; }
    public required AddonGameServer GameServer { get; init; }
    public required AddonPlayer Player { get; init; }
}