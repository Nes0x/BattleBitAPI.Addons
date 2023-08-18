using BattleBitAPI.Addons.CommandHandler.Common;
using BattleBitAPI.Addons.Common;
using BattleBitAPI.Addons.Examples.Modules.CommandModules.Checkers;
using BattleBitAPI.Addons.Examples.Services;

namespace BattleBitAPI.Addons.Examples.Modules.CommandModules;

[Command(Name = "warn")]
[AdminChecker]
public class WarnModule : CommandModule
{
    private readonly ConfigService _config;

    public WarnModule(ConfigService config)
    {
        _config = config;
    }

    [Command(Name = "add")]
    public Task<bool> HandleAdd(AddonPlayer target, string reason)
    {
        Console.WriteLine($"{reason} {Context.ChatChannel} {_config.AdminId}");
        return Task.FromResult(true);
    }
}