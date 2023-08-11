# Checker 

This is precondition, you can check any statement and allow execute command.

To create checker you must derive from Checker class and override RunCommand method.

```csharp
public class AdminChecker : CheckerAttribute<Player>
{
    //You can also use Context in checkers
    public override bool RunCommand()
    {
        return Context.Player.SteamID == 25242738782;
    }
}
```

You can apply checker for all module commands or for single command.


```csharp
//If you set checker on class range, then all of commands will have that checker
[AdminChecker]
public class AdminModule : CommandModule<Player>
{
    [Command(Name = "kill", Description = "Kill player.")]
    public Task HandleKill(Player target)
    {
        if (target is not null) target.Kill();
        return Task.CompletedTask;
    }
    
    [Command(Name = "kick")]
    public Task HandleKick(Player target, string reason)
    {
        if (target is not null) target.Kick(reason);
        return Task.CompletedTask;
    }
}
```

```csharp
public class AdminModule : CommandModule<Player>
{
    [Command(Name = "kill", Description = "Kill player.")]
    [AdminChecker]
    //If you set checker on method range, then only this command will be checked
    public Task HandleKill(Player target)
    {
        if (target is not null) target.Kill();
        return Task.CompletedTask;
    }
    
    [Command(Name = "kick")]
    public Task HandleKick(Player target, string reason)
    {
        if (target is not null) target.Kick(reason);
        return Task.CompletedTask;
    }
}
```
