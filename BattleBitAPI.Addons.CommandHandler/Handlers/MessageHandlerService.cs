using BattleBitAPI.Addons.CommandHandler.Common;
using BattleBitAPI.Addons.CommandHandler.Converters;
using BattleBitAPI.Common;

namespace BattleBitAPI.Addons.CommandHandler.Handlers;

public class MessageHandlerService<TPlayer> where TPlayer : Player
{
    private readonly CommandConverter<TPlayer> _commandConverter;
    private readonly CommandHandlerSettings _commandHandlerSettings;

    public MessageHandlerService(CommandHandlerSettings commandHandlerSettings,
        CommandConverter<TPlayer> commandConverter)
    {
        _commandHandlerSettings = commandHandlerSettings;
        _commandConverter = commandConverter;
    }

    public Task OnPlayerTypedMessage(TPlayer player, ChatChannel chatChannel, string content,
        Command<TPlayer> command, MethodRepresentation methodRepresentation)
    {
        var context = new Context<TPlayer>
        {
            Player = player,
            ChatChannel = chatChannel
        };

        var parametersFromCommand = content.Split(" ").ToList();
        var parametersFromMethod = methodRepresentation.MethodInfo.GetParameters();

        var result = _commandConverter.TryConvertParameters(parametersFromCommand, parametersFromMethod,
            methodRepresentation,
            context, out var convertedParameters);
        string message = null;
        switch (result)
        {
            case Result.Success:
            {
                context.ChangeContext(command);
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

        if (message is not null && player is not null) player.Message(message);

        return Task.CompletedTask;
    }
}