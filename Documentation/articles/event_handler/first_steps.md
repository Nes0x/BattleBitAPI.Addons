# Event Handler 
- You must add package `BattleBitAPI.Addons.EventHandler`.

To start, you must add EventHandler to host.

```csharp
var host = Host.CreateDefaultBuilder(args);
host.AddServerListener(2000)
    .AddEventHandler();
var app = host.Build();
await app.RunAsync();
```

After that, you can create class which listening to events.

```csharp
public class OnPlayer : EventModule
{
    //Parameter must be same as EventType. 
    [Event(EventType = EventType.OnPlayerConnected)]
    public Task HandleOnPlayerConnected(OnPlayerConnectedArgs args)
    {
        return Task.CompletedTask;
    }
}
```

List of all available events: [click](https://nes0x.github.io/BattleBitAPI.Addons/documentation/BattleBitAPI.Addons.EventHandler.Common.EventType.html)
