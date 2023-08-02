using BattleBitAPI.Common;

namespace BattleBitAPI.Addons.CommandHandler.Common;

public interface IContext<TPlayer> where TPlayer : Player
{
    TPlayer Player { get; init; }
    ChatChannel ChatChannel { get; init; }
}