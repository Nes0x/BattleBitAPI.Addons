using BattleBitAPI.Common;

namespace BattleBitAPI.Addons.EventHandler.Events;

public class OnPlayerTypedMessageArgs<TPlayer> where TPlayer : Player
{
    public TPlayer Player { get; internal init; }
    public ChatChannel ChatChannel { get; internal init; }
    public string Content { get; internal init; }
}