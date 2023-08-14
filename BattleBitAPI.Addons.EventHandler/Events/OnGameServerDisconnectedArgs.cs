using BattleBitAPI.Server;

namespace BattleBitAPI.Addons.EventHandler.Events;

public class OnGameServerDisconnectedArgs
{
    public GameServer GameServer { get; internal init; }
}