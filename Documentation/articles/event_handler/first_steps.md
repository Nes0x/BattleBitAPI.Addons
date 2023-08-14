# Event Handler 
- You must add package `BattleBitAPI.Addons.EventHandler`

To start, you must add EventHandler to host.

```csharp
var host = Host.CreateDefaultBuilder(args);
host.AddServerListener<Player>(2000)
    //In generic use same Player type as in ServerListener.
    .AddEventHandler<Player>();
var app = host.Build();
await app.RunAsync();
```

After that, you can create class which listening to events

```csharp
//In generic use same Player type as in ServerListener, EventHandler.
public class OnPlayer : EventModule<Player>
{
    //Parameter must be same as EventType. 
    [Event(EventType = EventType.OnPlayerConnected)]
    public Task HandleOnPlayerConnected(OnPlayerConnectedArgs<Player> args)
    {
        return Task.CompletedTask;
    }
}
```

List of all available events: [click](https://nes0x.github.io/BattleBitAPI.Addons/documentation/BattleBitAPI.Addons.EventHandler.Common.EventType.html)
