# Beginning
- You must have installed `Microsoft.Extensions.Hosting 7.0.1` in your project.
- You must using `.NET 7` or newer.
- You must install `BattleBitAPI.Addons.EventHandler` or `BattleBitAPI.Addons.CommandHandler` package from nuget.

First of all create host and add ServerListener.
```csharp
var host = Host.CreateDefaultBuilder(args);
host.AddServerListener(2000);
var app = host.Build();
await app.RunAsync();
```
