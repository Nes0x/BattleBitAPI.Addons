using BattleBitAPI.Addons.CommandHandler.Common;
using BattleBitAPI.Addons.CommandHandler.Converters;
using BattleBitAPI.Common;
using Microsoft.Extensions.Logging;

namespace BattleBitAPI.Addons.CommandHandler.Handlers;

public class MessageHandlerService<TPlayer> : IMessageHandler<TPlayer> where TPlayer : Player
{
    private readonly CommandHandlerSettings _commandHandlerSettings;
    private readonly IConverter<TPlayer> _converter;
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
        CommandModule<TPlayer> commandModule, Command command)
    {
        if (!content.StartsWith(
                $"{_commandHandlerSettings.CommandRegex.ToLower()}{command.CommandName.ToLower()}"))
            return Task.CompletedTask;

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

        var result = _converter.TryConvertParameters(parametersFromCommand,
            command,
            context, out var convertedParameters);
        string message = null;
        switch (result)
        {
            case Result.Success:
            {
                commandModule.Context = context;
                try
                {
                    command.MethodInfo.Invoke(commandModule, convertedParameters.ToArray());
                }
                catch (Exception e)
                {
                    _logger.LogError(e.Message, e);
                    message = e.Message;
                }

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