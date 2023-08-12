# Command Handler
- You must add package `BattleBitAPI.Addons.CommandHandler`

Add CommandHandler to created host
```csharp
var host = Host.CreateDefaultBuilder(args);
//In generic use your Player type, in parameters pass port and IPAddress.
host.AddServerListener<Player>(2000)
    //In generic use same Player type as in ServerListener.
    //In parameters add CommandHandlerSettings and create change properties if you want.
     .AddCommandHandler<Player>(new CommandHandlerSettings
    {
        CommandRegex = "."
    });
var app = host.Build();
await app.RunAsync();
```

# First module

To create commands you must derive from CommandModule class

You can create multiple commands in one class. 
Commands can be with same name, but must have inner parameters count or types.

```csharp
//Generic type must be same as you typed in host.
public class AdminModule : CommandModule<Player>
{
    //usage - .kill steamIdPlayer
    //For usage Player parameter in command method you must add TypeReaders
    [Command(Name = "kill", Description = "Kill player.")]
    public Task HandleKill(Player target)
    {
        if (target is not null) target.Kill();
        return Task.CompletedTask;
    }
    
    //usage - .kick steamIdPlayer reason
    [Command(Name = "kick")]
    public Task HandleKick(Player target, string reason)
    {
        if (target is not null) target.Kick(reason);
        return Task.CompletedTask;
    }
}
```
