# Parameters

You can make command which has default parameter value 

```csharp
//use - .kick steamIdPlayer myReason or .kick steamIdPlayer
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
public Task HandleKick(Player target, string reason = "example")
{
    Console.WriteLine($"{Context.Player} {Context.ChatChannel} {Context.GameServer}");
    if (target is not null) target.Kick(reason);
    return Task.CompletedTask;
}
```

You can specify parameters name in default help command by `[CommandParameter]`

```csharp
[Command(Name = "kick")]
public Task HandleKick([CommandParameter(Name = "gracz")]Player target, string reason = "example")
{
    if (target is not null) target.Kick(reason);
    return Task.CompletedTask;
}
```