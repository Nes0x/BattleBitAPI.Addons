# Event Handler 

To start, you must add EventHandler to host.

```csharp
var host = Host.CreateDefaultBuilder(args);
host.AddServerListener<Player>(2000)
    //In generic use same Player type as in ServerListener.
    .AddEventHandler<Player>();
var app = host.Build();
await app.RunAsync();
```

After that, you can create class which listening to event

```csharp
//In generic use same Player type as in ServerListener, EventHandler.
public class OnPlayerConnected : OnPlayerConnectedHandler<Player>
{
    public OnPlayerConnected(ServerListener<Player> serverListener) : base(serverListener)
    {
    }

    protected override Task Handle(Player arg)
    {
        //do anything
        throw new NotImplementedException();
    }
}
```

List of all available events: [click](https://nes0x.github.io/BattleBitAPI.Addons/documentation/BattleBitAPI.Addons.EventHandler.Events.Handlers.html)
