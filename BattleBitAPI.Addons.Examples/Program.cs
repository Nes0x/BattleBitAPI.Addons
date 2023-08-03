﻿using BattleBitAPI;
using BattleBitAPI.Addons.CommandHandler.Common;
using BattleBitAPI.Addons.CommandHandler.Extensions;
using BattleBitAPI.Addons.Common.Extensions;
using BattleBitAPI.Addons.EventHandler.Extensions;
using Microsoft.Extensions.Hosting;

var host = Host.CreateDefaultBuilder(args);
host.AddServerListener<Player>(2000).AddTypeReaders<Player>().AddEventHandlers<Player>()
    .AddCommandHandler<Player>(new CommandHandlerSettings());
var app = host.Build();
await app.RunAsync();