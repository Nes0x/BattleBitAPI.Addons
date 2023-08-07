using BattleBitAPI.Common;
using BattleBitAPI.Server;

namespace BattleBitAPI.Addons.CommandHandler.Common;

public interface IContext<TPlayer> where TPlayer : Player
{
    TPlayer Player { get; init; }
    ChatChannel ChatChannel { get; init; }
    GameServer GameServer { get; init; }
    IServiceProvider ServiceProvider { get; init; }
}