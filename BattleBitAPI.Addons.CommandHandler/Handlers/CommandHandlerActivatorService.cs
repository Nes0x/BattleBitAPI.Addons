using System.Reflection;
using BattleBitAPI.Addons.CommandHandler.Common;
using BattleBitAPI.Addons.CommandHandler.Validations;
using BattleBitAPI.Common;
using BattleBitAPI.Server;
using Microsoft.Extensions.Hosting;

namespace BattleBitAPI.Addons.CommandHandler.Handlers;

public class CommandHandlerActivatorService<TPlayer> : IHostedService where TPlayer : Player
{
    private readonly IEnumerable<CommandModule<TPlayer>> _commandModules;
    private readonly List<Func<TPlayer, ChatChannel, string, Task>> _handlers;
    private readonly IMessageHandler<TPlayer> _messageHandler;
    private readonly ServerListener<TPlayer> _serverListener;
    private readonly IValidator<TPlayer> _validator;

    public CommandHandlerActivatorService(IEnumerable<CommandModule<TPlayer>> commandModules,
        IMessageHandler<TPlayer> messageHandler, ServerListener<TPlayer> serverListener, IValidator<TPlayer> validator)
    {
        _commandModules = commandModules;
        _messageHandler = messageHandler;
        _handlers = new List<Func<TPlayer, ChatChannel, string, Task>>();
        _serverListener = serverListener;
        _validator = validator;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        ModifyCommandModules();
        foreach (var commandModule in _commandModules)
        foreach (var command in commandModule.Commands)
        {
            Func<TPlayer, ChatChannel, string, Task> handler = (player, channel, content) =>
                _messageHandler.OnPlayerTypedMessage(player, channel, content.Trim(), commandModule, command);
            _handlers.Add(handler);
            _serverListener.OnPlayerTypedMessage += handler;
        }

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        foreach (var handler in _handlers) _serverListener.OnPlayerTypedMessage -= handler;
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
                var commandName = "";
                var removeParameters = 1;
                var commandAttribute = commandMethod.GetCustomAttribute<CommandAttribute>()!;
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