using BattleBitAPI.Addons.CommandHandler.Common;
using BattleBitAPI.Addons.CommandHandler.Converters;
using BattleBitAPI.Common;
using Microsoft.Extensions.Logging;

namespace BattleBitAPI.Addons.CommandHandler.Handlers;

public class MessageHandlerService<TPlayer> : IMessageHandler<TPlayer> where TPlayer : Player 
{
    private readonly IConverter<TPlayer> _converter;
    private readonly CommandHandlerSettings _commandHandlerSettings;
    private readonly ILogger<MessageHandlerService<TPlayer>> _logger;
    private readonly IServiceProvider _provider;

    public MessageHandlerService(CommandHandlerSettings commandHandlerSettings,
        IConverter<TPlayer> converter, ILogger<MessageHandlerService<TPlayer>> logger, IServiceProvider provider)
    {
        _commandHandlerSettings = commandHandlerSettings;
        _converter = converter;
        _logger = logger;
        _provider = provider;
    }

    public Task OnPlayerTypedMessage(TPlayer player, ChatChannel chatChannel, string content,
        Command<TPlayer> command, MethodRepresentation methodRepresentation)
    {
        if (player is null)
        {
            _logger.LogError("The player is null.");
            return Task.CompletedTask;
        }

        var context = new Context<TPlayer>
        {
            Player = player,
            ChatChannel = chatChannel,
            GameServer = player.GameServer,
            ServiceProvider = _provider
        };

        var parametersFromCommand = content.Split(" ").ToList();
        var parametersFromMethod = methodRepresentation.MethodInfo.GetParameters();

        var result = _converter.TryConvertParameters(parametersFromCommand, parametersFromMethod,
            methodRepresentation,
            context, out var convertedParameters);
        string message = null;
        switch (result)
        {
            case Result.Success:
            {
                command.Context = context;
                if (content.StartsWith(
                        $"{_commandHandlerSettings.CommandRegex.ToLower()}{methodRepresentation.CommandName.ToLower()}"))
                    methodRepresentation.MethodInfo.Invoke(command, convertedParameters.ToArray());
                break;
            }
            case Result.Error:
                message = _commandHandlerSettings.ErrorCallback;
                break;
            case Result.Checker:
                message = _commandHandlerSettings.CheckerCallback;
                break;
        }

        if (message is not null) player.Message(message);

        return Task.CompletedTask;
    }
}