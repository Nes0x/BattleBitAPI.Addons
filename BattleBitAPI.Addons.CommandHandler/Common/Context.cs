using BattleBitAPI.Common;
using BattleBitAPI.Server;

namespace BattleBitAPI.Addons.CommandHandler.Common;

public class Context<TPlayer> where TPlayer : Player
{
    public TPlayer Player { get; internal init; }
    public ChatChannel ChatChannel { get; internal init; }
    public GameServer GameServer { get; internal init; }
    public IServiceProvider ServiceProvider { get; internal init; }
}