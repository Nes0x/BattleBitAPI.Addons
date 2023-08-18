# Type Readers

This thing is responsible for handling custom parameters in command. 

If you want to use default custom type readers or you create own type reader, you can add them like that:

```csharp
var host = Host.CreateDefaultBuilder(args);
host.AddServerListener(2000)
    //In parameters add CommandHandlerSettings and change properties if you want.
     .AddCommandHandler(new CommandHandlerSettings
    {
        CommandRegex = "."
    })
    .AddTypeReaders();
var app = host.Build();
await app.RunAsync();
```

# Custom type reader

You can create own type reader by deriving from TypeReader class. 

Also you can make multiply type readers for one type.

For example: 

```csharp
public class AddonPlayerNickTypeReader : TypeReader
{
    //You need to specify the type in the constructor to be handled as custom.
    public AddonPlayerNickTypeReader() : base(typeof(AddonPlayer))
    {
    }

    //You can use Context in this method.
    //In returning type you must use type which you definied in the constructor.
    public override AddonPlayer? ChangeType(object obj)
    {
        return Context.GameServer.GetAllPlayers().FirstOrDefault(p => p.Name == obj.ToString());
    }
}
```