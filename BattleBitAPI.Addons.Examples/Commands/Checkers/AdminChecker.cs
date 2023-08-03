using BattleBitAPI.Addons.CommandHandler.Common;

namespace BattleBitAPI.Addons.Examples.Commands.Checkers;

public class AdminChecker : CheckerAttribute<Player>
{
    public override bool RunCommand()
    {
        return true;
    }
}