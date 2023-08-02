using System.Reflection;
using BattleBitAPI.Addons.CommandHandler.Common;
using BattleBitAPI.Common;
using BattleBitAPI.Server;
using Microsoft.Extensions.Hosting;

namespace BattleBitAPI.Addons.CommandHandler.Handlers;

public class CommandHandlerActivatorService<TPlayer> : IHostedService where TPlayer : Player
{
    private readonly IEnumerable<Command<TPlayer>> _commands;
    private readonly MessageHandlerService<TPlayer> _messageHandler;
    private readonly List<Command<TPlayer>> _modifiedCommands;
    private readonly List<Func<TPlayer, ChatChannel, string, Task>> _playerTypedMessageHandlers;
    private readonly ServerListener<TPlayer> _serverListener;

    public CommandHandlerActivatorService(ServerListener<TPlayer> serverListener,
        IEnumerable<Command<TPlayer>> commands, MessageHandlerService<TPlayer> messageHandler)
    {
        _serverListener = serverListener;
        _commands = commands;
        _messageHandler = messageHandler;
        _modifiedCommands = new List<Command<TPlayer>>();
        _playerTypedMessageHandlers = new List<Func<TPlayer, ChatChannel, string, Task>>();
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        PopulateModifiedCommands();
        foreach (var command in _modifiedCommands)
        foreach (var methodHandler in command.MethodHandlers)
        {
            Func<TPlayer, ChatChannel, string, Task> handler = (player, channel, content) =>
                _messageHandler.OnPlayerTypedMessage(player, channel, content, command, methodHandler);
            _playerTypedMessageHandlers.Add(handler);
            _serverListener.OnPlayerTypedMessage += handler;
        }

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        foreach (var handler in _playerTypedMessageHandlers) _serverListener.OnPlayerTypedMessage -= handler;
        _playerTypedMessageHandlers.Clear();

        return Task.CompletedTask;
    }

    private void PopulateModifiedCommands()
    {
        var commandNames = new List<string>();
        foreach (var command in _commands)
        {
            var methodHandlers = new List<MethodRepresentation>();
            var methods = command.GetType().GetMethods()
                .Where(m => m.GetCustomAttributes(typeof(CommandAttribute), false).Length > 0)
                .ToArray();

            foreach (var methodInfo in methods)
            {
                var commandName = methodInfo.GetCustomAttribute<CommandAttribute>()!.Name;
                if (commandNames.Contains(commandName)) continue;
                methodHandlers.Add(new MethodRepresentation
                {
                    MethodInfo = methodInfo,
                    CommandName = commandName
                });
                commandNames.Add(commandName);
                command.MethodHandlers = methodHandlers;
                _modifiedCommands.Add(command);
            }
        }
    }
}