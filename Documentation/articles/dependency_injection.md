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
host.AddServerListener(2000)
    .AddTypeReaders()
    .AddEventHandler()
    .AddCommandHandler(new CommandHandlerSettings
    {
        CommandRegex = "."
    }).ConfigureServices((_, services) => { services.AddSingleton(ConfigService.Create()); });

var app = host.Build();
await app.RunAsync();
```

```csharp
public class AdminChecker : CheckerAttribute
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
public class WarnModule : CommandModule
{
    private readonly ConfigService _config;

    public WarnModule(ConfigService config)
    {
        _config = config;
    }

    [Command(Name = "add")]
    public Task<bool> HandleAdd(AddonPlayer target, string reason)
    {
        Console.WriteLine($"{_config.AdminId}");
        return Task.CompletedTask;
    }
}
```
