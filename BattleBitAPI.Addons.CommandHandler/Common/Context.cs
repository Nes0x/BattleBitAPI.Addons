using BattleBitAPI.Addons.Common;
using BattleBitAPI.Common;
using BattleBitAPI.Server;

namespace BattleBitAPI.Addons.CommandHandler.Common;

public class Context
{
    public AddonPlayer Player { get; internal init; }
    public ChatChannel ChatChannel { get; internal init; }
    public GameServer<AddonPlayer> GameServer { get; internal init; }
    public IServiceProvider ServiceProvider { get; internal init; }
}