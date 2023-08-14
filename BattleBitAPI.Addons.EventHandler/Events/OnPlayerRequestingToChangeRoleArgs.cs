using BattleBitAPI.Common;

namespace BattleBitAPI.Addons.EventHandler.Events;

public class OnPlayerRequestingToChangeRoleArgs<TPlayer> where TPlayer : Player
{
    public TPlayer Player { get; internal init; }
    public GameRole GameRole { get; internal init; }
}