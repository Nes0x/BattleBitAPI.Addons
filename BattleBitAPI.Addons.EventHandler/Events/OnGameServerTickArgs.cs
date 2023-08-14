using BattleBitAPI.Server;

namespace BattleBitAPI.Addons.EventHandler.Events;

public class OnGameServerTickArgs
{
    public GameServer GameServer { get; internal init; }
}