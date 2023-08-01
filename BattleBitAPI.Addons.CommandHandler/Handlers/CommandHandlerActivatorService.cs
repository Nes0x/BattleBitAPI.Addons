using System.Reflection;
using BattleBitAPI.Common;
using BattleBitAPI.Server;
using Microsoft.Extensions.Hosting;

namespace BattleBitAPI.Addons.CommandHandler.Handlers;

public class CommandHandlerActivatorService<TPlayer> : IHostedService where TPlayer : Player
{
    private readonly CommandHandlerSettings _commandHandlerSettings;
    private readonly IEnumerable<Command<TPlayer>> _commands;
    private readonly ServerListener<TPlayer> _serverListener;


    public CommandHandlerActivatorService(ServerListener<TPlayer> serverListener,
        IEnumerable<Command<TPlayer>> commands, CommandHandlerSettings commandHandlerSettings)
    {
        _serverListener = serverListener;
        _commands = commands;
        _commandHandlerSettings = commandHandlerSettings;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        foreach (var command in _commands)
        {
            var methods = command.GetType().GetMethods()
                .Where(m => m.GetCustomAttributes(typeof(CommandAttribute), false).Length > 0)
                .ToArray();
            foreach (var methodInfo in methods)
                _serverListener.OnPlayerTypedMessage += (player, channel, content) =>
                {
                    return OnPlayerTypedMessageAsync(player, channel, content, command,
                        methodInfo.GetCustomAttribute<CommandAttribute>().Name, methodInfo);
                };
        }

        await Task.CompletedTask;
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
    }

    private async Task OnPlayerTypedMessageAsync(TPlayer player, ChatChannel chatChannel, string content,
        Command<TPlayer> command,
        string commandName,
        MethodInfo methodInfo
    )
    {
        command.Context = new Context<TPlayer>
        {
            Player = player,
            ChatChannel = chatChannel
        };
        var parametersFromCommand = content.Split(" ").ToList();
        parametersFromCommand.RemoveAt(0);
        var methodParameters = methodInfo.GetParameters();
        if (parametersFromCommand.Count != methodParameters.Length)
        {
            player.Message(_commandHandlerSettings.ErrorCallback);
            return;
        }

        var convertedParameters = new List<object>();
        for (var i = 0; i < methodParameters.Length; i++)
            try
            {
                convertedParameters.Add(Convert.ChangeType(parametersFromCommand[i],
                    methodParameters[i].ParameterType));
            }
            catch (Exception)
            {
                player.Message(_commandHandlerSettings.ErrorCallback);
                return;
            }

        if (content.StartsWith($"{_commandHandlerSettings.CommandRegex.ToLower()}{commandName.ToLower()}"))
            methodInfo.Invoke(command, convertedParameters.ToArray());
    }
}