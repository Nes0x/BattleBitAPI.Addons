using System.Reflection;
using BattleBitAPI.Addons.CommandHandler.Common;
using BattleBitAPI.Addons.CommandHandler.Converters;
using BattleBitAPI.Addons.CommandHandler.Validations;
using BattleBitAPI.Addons.Common;
using BattleBitAPI.Common;
using BattleBitAPI.Server;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace BattleBitAPI.Addons.CommandHandler.Handlers;

public class CommandHandlerActivatorService : IHostedService
{
    public static readonly List<CommandModule> CommandModules = new();
    private readonly IEnumerable<CommandModule> _commandModules;
    private readonly ServerListener<AddonPlayer, AddonGameServer> _serverListener;
    private readonly CommandHandlerSettings _commandHandlerSettings;
    private readonly IConverter _converter;
    private readonly IValidator _validator;
    private readonly ILogger<MessageHandlerService> _logger;
    private readonly IServiceProvider _provider;
    private readonly List<Func<MessageHandlerService>> _handlers;

    public CommandHandlerActivatorService(IEnumerable<CommandModule> commandModules,
        IValidator validator, ServerListener<AddonPlayer, AddonGameServer> serverListener, CommandHandlerSettings commandHandlerSettings, IConverter converter, ILogger<MessageHandlerService> logger, IServiceProvider provider)
    {
        _commandModules = commandModules;
        _validator = validator;
        _serverListener = serverListener;
        _commandHandlerSettings = commandHandlerSettings;
        _converter = converter;
        _logger = logger;
        _provider = provider;
        _handlers = new List<Func<MessageHandlerService>>();
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        ModifyCommandModules();
        foreach (var commandModule in _commandModules)
        {
            CommandModules.Add(commandModule);
            foreach (var command in commandModule.Commands)
            {
                var handler = () => new MessageHandlerService(_commandHandlerSettings, _converter, _logger, _provider,
                    commandModule, command);
                _handlers.Add(handler);
                _serverListener.OnCreatingGameServerInstance += handler;
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