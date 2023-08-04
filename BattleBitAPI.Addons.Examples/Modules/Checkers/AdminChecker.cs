using BattleBitAPI.Addons.CommandHandler.Common;

namespace BattleBitAPI.Addons.Examples.Modules.Checkers;

public class AdminChecker : CheckerAttribute<Player>
{
    public override bool RunCommand()
    {
        return Context.Player.SteamID == 24929842942;
    }
}