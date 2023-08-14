using BattleBitAPI.Server;

namespace BattleBitAPI.Addons.EventHandler.Events;

public class OnGameServerReconnectedArgs
{
    public GameServer GameServer { get; internal init; }
}