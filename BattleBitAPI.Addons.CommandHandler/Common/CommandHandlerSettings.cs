namespace BattleBitAPI.Addons.CommandHandler.Common;

public class CommandHandlerSettings
{
    public string ErrorCallback { get; init; } =
        "You typed arguments in a wrong way or you can't execute this command.";

    public string CommandRegex { get; init; } = "/";
}