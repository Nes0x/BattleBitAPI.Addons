# Parameters

You can make command which has default parameter value 

```csharp
//usage - .kick steamIdPlayer myReason or .kick steamIdPlayer
[Command(Name = "kick")]
public Task HandleKick(Player target, string reason = "example")
{
    if (target is not null) target.Kick(reason);
    return Task.CompletedTask;
}
```

You can get information from command by using Context 

```csharp
[Command(Name = "kick")]
public Task HandleKick(Player target, string reason)
{
    Console.WriteLine($"{Context.Player} {Context.ChatChannel} {Context.GameServer}");
    if (target is not null) target.Kick(reason);
    return Task.CompletedTask;
}
```

You can specify parameters name in default help command by `[CommandParameter]`

```csharp
[Command(Name = "kick")]
public Task HandleKick([CommandParameter(Name = "gracz")]Player target, string reason)
{
    if (target is not null) target.Kick(reason);
    return Task.CompletedTask;
}
```

You can add description command which will be displayed in default help command

```csharp
[Command(Name = "kick", Description = "Kick player from the server.")]
public Task HandleKick(Player target, string reason)
{
    if (target is not null) target.Kick(reason);
    return Task.CompletedTask;
}
```