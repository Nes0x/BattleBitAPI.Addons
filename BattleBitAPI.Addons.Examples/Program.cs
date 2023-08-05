using BattleBitAPI;
using BattleBitAPI.Addons.CommandHandler.Common;
using BattleBitAPI.Addons.CommandHandler.Extensions;
using BattleBitAPI.Addons.Common.Extensions;
using BattleBitAPI.Addons.EventHandler.Extensions;
using BattleBitAPI.Addons.Examples.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = Host.CreateDefaultBuilder(args);
host.AddServerListener<Player>(2000).AddTypeReaders<Player>().AddEventHandler<Player>()
    .AddCommandHandler<Player>(new CommandHandlerSettings
    {
        CommandRegex = "."
    }).ConfigureServices((_, services) =>
    {
        services.AddSingleton(ConfigService.Create());
    });

var app = host.Build();
await app.RunAsync();