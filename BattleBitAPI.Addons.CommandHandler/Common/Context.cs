using BattleBitAPI.Common;

namespace BattleBitAPI.Addons.CommandHandler.Common;

public class Context<TPlayer> : IContext<TPlayer> where TPlayer : Player
{
    public TPlayer Player { get; init; }
    public ChatChannel ChatChannel { get; init; }
}