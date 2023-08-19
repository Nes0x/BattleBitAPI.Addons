using BattleBitAPI.Addons.Common;
using BattleBitAPI.Addons.EventHandler.Common;
using BattleBitAPI.Common;

namespace BattleBitAPI.Addons.EventHandler.Events;

public class OnPlayerJoiningToServerEvent : EventGameServer
{
    public OnPlayerJoiningToServerEvent(EventModule eventModule, Event @event) : base(eventModule, @event)
    {
    }

    public override Task OnPlayerJoiningToServer(ulong steamId, PlayerJoiningArguments args)
    {
        return (Task)Event.MethodInfo.Invoke(EventModule, new[]
        {
            new OnPlayerJoiningToServerArgs()
            {
                SteamId = steamId,
                PlayerJoiningArguments = args,
                GameServer = this
            }
        
        });
    }
}

public class OnPlayerJoiningToServerArgs : IGameServerArgs
{
    public required ulong SteamId { get; init; }
    public required PlayerJoiningArguments PlayerJoiningArguments { get; init; }
    public required AddonGameServer GameServer { get; init; }
}