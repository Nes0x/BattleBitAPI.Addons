﻿using BattleBitAPI.Addons.CommandHandler.Common;
using BattleBitAPI.Addons.Examples.Modules.Checkers;
using BattleBitAPI.Addons.Examples.Services;

namespace BattleBitAPI.Addons.Examples.Modules;

[Command(Name = "warn")]
[AdminChecker]
public class WarnModule : CommandModule<Player>
{
    private readonly ConfigService _config;

    public WarnModule(ConfigService config)
    {
        _config = config;
    }

    [Command(Name = "add")]
    public Task HandleAddAsync(Player target, string reason)
    {
        Console.WriteLine($"{reason} {Context.ChatChannel} {_config.AdminId}");
        return Task.CompletedTask;
    }
    
    [Command(Name = "add")]
    public Task HandleAddAsync(ulong target, string reason)
    {
        Console.WriteLine($"{reason} {target} {Context.ChatChannel} {_config.AdminId}");
        return Task.CompletedTask;
    }
    
    [Command(Name = "remove")]
    public Task HandleRemoveAsync(Player target, string reason)
    {
        Console.WriteLine($"{reason} {target.Name}");
        return Task.CompletedTask;
    }
}