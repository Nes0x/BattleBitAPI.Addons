using BattleBitAPI.Server;

namespace BattleBitAPI.Addons.EventHandler.Events;

public class OnGameServerConnectedArgs
{
    public GameServer GameServer { get; internal init; }
}