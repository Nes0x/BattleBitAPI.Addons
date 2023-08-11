# Type Readers

This thing is responsible for handling custom parameters in command. 

If you want to use default custom type readers or you create own type reader, you can add them like that:

```csharp
var host = Host.CreateDefaultBuilder(args);
//In generic use your Player type, in parameters pass port and IPAddress.
host.AddServerListener<Player>(2000)
    //In generic use same Player type as in ServerListener.
    //In parameters add CommandHandlerSettings and create change properties if you want.
     .AddCommandHandler<Player>(new CommandHandlerSettings
    {
        CommandRegex = "."
    })
    //In generic use same Player type as in ServerListener.
    .AddTypeReaders<Player>();
var app = host.Build();
await app.RunAsync();
```

# Custom type reader

You can create own type reader by deriving from TypeReader class. 

Also you can make multiply type readers for one type.

For example: 

```csharp
//Generic is your Player type.
public class PlayerNickTypeReader : TypeReader<Player>
{
    //You need to specify the type in the constructor to be handled as custom.
    public PlayerNickTypeReader() : base(typeof(Player))
    {
    }

    //You can use Context in this method.
    //In returning type you must use type which you definied in the constructor.
    public override Player? ChangeType(object obj)
    {
        return Context.GameServer.GetAllPlayers().FirstOrDefault(p => p.Name == obj.ToString());
    }
}
```