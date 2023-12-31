﻿using System.Net;
using System.Reflection;
using BattleBitAPI.Addons.CommandHandler.Common;
using BattleBitAPI.Addons.CommandHandler.Converters;
using BattleBitAPI.Addons.CommandHandler.Validations;
using BattleBitAPI.Addons.Common;
using BattleBitAPI.Server;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace BattleBitAPI.Addons.CommandHandler.Handlers;

public class CommandHandlerActivatorService : IHostedService
{
    public static readonly List<CommandModule> CommandModules = new();
    private readonly CommandHandlerSettings _commandHandlerSettings;
    private readonly IEnumerable<CommandModule> _commandModules;
    private readonly IConverter _converter;
    private readonly List<Func<IPAddress, ushort, AddonGameServer>> _handlers;
    private readonly ILogger<MessageEvent> _logger;
    private readonly IServiceProvider _provider;
    private readonly ServerListener<AddonPlayer, AddonGameServer> _serverListener;
    private readonly IValidator _validator;

    public CommandHandlerActivatorService(IEnumerable<CommandModule> commandModules,
        IValidator validator, ServerListener<AddonPlayer, AddonGameServer> serverListener,
        CommandHandlerSettings commandHandlerSettings, IConverter converter, ILogger<MessageEvent> logger,
        IServiceProvider provider)
    {
        _commandModules = commandModules;
        _validator = validator;
        _serverListener = serverListener;
        _commandHandlerSettings = commandHandlerSettings;
        _converter = converter;
        _logger = logger;
        _provider = provider;
        _handlers = new List<Func<IPAddress, ushort, AddonGameServer>>();
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        ModifyCommandModules();
        foreach (var commandModule in _commandModules)
        {
            CommandModules.Add(commandModule);
            foreach (var command in commandModule.Commands)
            {
                Func<IPAddress, ushort, AddonGameServer> handler = (ipAddress, port) => new MessageEvent(
                    _commandHandlerSettings, _converter,
                    _logger, _provider,
                    commandModule, command);
                _serverListener.OnCreatingGameServerInstance += handler;
                _handlers.Add(handler);
            }
        }

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        foreach (var handler in _handlers) _serverListener.OnCreatingGameServerInstance -= handler;
        _handlers.Clear();
        return Task.CompletedTask;
    }

    private void ModifyCommandModules()
    {
        foreach (var commandModule in _commandModules)
        {
            var commands = new List<Command>();
            var commandMethods = commandModule.GetType().GetMethods()
                .Where(m => m.GetCustomAttributes(typeof(CommandAttribute), false).Length > 0)
                .ToArray();

            foreach (var commandMethod in commandMethods)
            {
                var removeParameters = 1;
                var commandAttribute = commandMethod.GetCustomAttribute<CommandAttribute>()!;
                var commandName = $"{commandAttribute.Name}";
                var commandAttributeClass = commandMethod.DeclaringType.GetCustomAttribute<CommandAttribute>();
                if (commandAttributeClass is not null)
                {
                    commandName = $"{commandAttributeClass.Name} {commandAttribute.Name}".Trim();
                    removeParameters = 2;
                }

                var command = new Command
                {
                    MethodInfo = commandMethod,
                    CommandName = commandName,
                    CommandDescription = commandAttribute.Description.Trim(),
                    Parameters = commandMethod.GetParameters(),
                    RemoveParameters = removeParameters
                };
                if (!_validator.ValidateUniqueCommand(command, commands)) continue;
                commands.Add(command);
            }

            commandModule.Commands = commands;
        }
    }
}