using BattleBitAPI.Addons.Common;

namespace BattleBitAPI.Addons.EventHandler.Events;

public interface IPlayerArgs
{
    public AddonPlayer Player { get; init; }
}