# Command Handler
- You must add package `BattleBitAPI.Addons.CommandHandler`

Add CommandHandler to created host.
```csharp
var host = Host.CreateDefaultBuilder(args);
//In parameters pass port and IPAddress.
host.AddServerListener(2000)
    //In parameters add CommandHandlerSettings and change properties if you want.
     .AddCommandHandler(new CommandHandlerSettings
    {
        CommandRegex = "."
    });
var app = host.Build();
await app.RunAsync();
```

# First module

To create commands you must derive from CommandModule class.

You can create multiple commands in one class. 
Commands can be with same name, but must have inner parameters count or types.

```csharp
public class AdminModule : CommandModule
{
    //usage - .kill steamIdPlayer
    //For usage AddonPlayer parameter in command method you must add own TypeReader.
    //You must always return Task<bool> type which specifies whether the command should be sent to the chat.
    [Command(Name = "kill", Description = "Kill player.")]
    public Task<bool> HandleKill(AddonPlayer target)
    {
        if (target is not null) target.Kill();
        return Task.FromResult(true);
    }
    
    //usage - .kick steamIdPlayer reason
    [Command(Name = "kick")]
    public Task<bool> HandleKick(AddonPlayer target, string reason)
    {
        if (target is not null) target.Kick(reason);
        return Task.FromResult(true);
    }
}
```
