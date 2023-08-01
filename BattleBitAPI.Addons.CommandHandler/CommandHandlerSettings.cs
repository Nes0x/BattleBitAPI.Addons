namespace BattleBitAPI.Addons.CommandHandler;

public class CommandHandlerSettings
{
    public string ErrorCallback { get; init; } = "You typed arguments in a wrong way.";
    public string CommandRegex { get; init; } = "/";
}