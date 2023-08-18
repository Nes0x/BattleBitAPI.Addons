using BattleBitAPI.Addons.Common;

namespace BattleBitAPI.Addons.EventHandler.Events;

public interface IGameServerArgs
{
    public AddonGameServer GameServer { get; init; }
}