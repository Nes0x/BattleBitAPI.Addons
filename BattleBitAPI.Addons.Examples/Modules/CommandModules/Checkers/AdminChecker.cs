using BattleBitAPI.Addons.CommandHandler.Common;
using BattleBitAPI.Addons.Examples.Services;
using Microsoft.Extensions.DependencyInjection;

namespace BattleBitAPI.Addons.Examples.Modules.CommandModules.Checkers;

public class AdminChecker : CheckerAttribute<Player>
{
    public override bool RunCommand()
    {
        var config = Context.ServiceProvider.GetRequiredService<ConfigService>();
        return Context.Player.SteamID == config.AdminId;
    }
}