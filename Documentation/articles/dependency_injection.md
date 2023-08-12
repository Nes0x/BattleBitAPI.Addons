# Dependecy Injection

You can create own service and use it in all command modules, events, checkers and type readers.

```csharp
public class ConfigService
{
    public ulong AdminId { get; init; }

    public static ConfigService Create()
    {
        return JsonSerializer.Deserialize<ConfigService>(File.ReadAllText("appsettings.json"));
    }
}
```

```csharp
var host = Host.CreateDefaultBuilder(args);
host.AddServerListener<Player>(2000)
    .AddTypeReaders<Player>()
    .AddEventHandler<Player>()
    .AddCommandHandler<Player>(new CommandHandlerSettings
    {
        CommandRegex = "."
    }).ConfigureServices((_, services) => { services.AddSingleton(ConfigService.Create()); });

var app = host.Build();
await app.RunAsync();
```

```csharp
public class AdminChecker : CheckerAttribute<Player>
{
    public override bool RunCommand()
    {
        var config = Context.ServiceProvider.GetRequiredService<ConfigService>();
        return Context.Player.SteamID == config.AdminId;
    }
}
```

```csharp
[Command(Name = "warn")]
[AdminChecker]
public class WarnModule : CommandModule<Player>
{
    private readonly ConfigService _config;

    public WarnModule(ConfigService config)
    {
        _config = config;
    }

    [Command(Name = "add")]
    public Task HandleAdd(Player target, string reason)
    {
        Console.WriteLine($"{reason} {Context.ChatChannel} {_config.AdminId}");
        return Task.CompletedTask;
    }
}
```
