namespace BattleBitAPI.Addons.CommandHandler.Common;

public class CommandHandlerSettings
{
    public string ErrorCallback { get; init; } =
        "You typed arguments in a wrong way.";

    public string CheckerCallback { get; init; } =
        "You can't execute this command.";

    public string CommandRegex { get; init; } = "/";

    public bool DefaultHelpCommand { get; init; } = true;
}