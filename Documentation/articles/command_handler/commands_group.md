# Commands Group

You can create one base command and subcommands.


```csharp
[Command(Name = "warn")]
public class WarnModule : CommandModule
{

    [Command(Name = "add")]
    //usage - .warn add steamIdPlayer reason
    public Task<bool> HandleAdd(AddonPlayer target, string reason)
    {
        return Task.FromResult(true);
    }
    
    [Command(Name = "remove")]
    //usage - .warn remove steamIdPlayer reason
    public Task<bool> HandleRemove(AddonPlayer target, string reason)
    {
        return Task.FromResult(true);
    }
}
```

