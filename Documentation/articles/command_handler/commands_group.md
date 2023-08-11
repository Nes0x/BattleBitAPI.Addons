# Commands Group

You can create one base command and subcommands.


```csharp
[Command(Name = "warn")]
public class WarnModule : CommandModule<Player>
{

    [Command(Name = "add")]
    //usage - .warn add steamIdPlayer reason
    public Task HandleAdd(Player target, string reason)
    {
        return Task.CompletedTask;
    }
    
    [Command(Name = "remove")]
    //usage - .warn remove steamIdPlayer reason
    public Task HandleRemove(Player target, string reason)
    {
        return Task.CompletedTask;
    }
}
```

