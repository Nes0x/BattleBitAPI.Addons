using BattleBitAPI.Addons.CommandHandler.Common;
using BattleBitAPI.Addons.CommandHandler.Converters;
using BattleBitAPI.Addons.Common;
using BattleBitAPI.Common;
using Microsoft.Extensions.Logging;

namespace BattleBitAPI.Addons.CommandHandler.Handlers;

public class MessageHandlerService : AddonGameServer
{
    private readonly Command _command;
    private readonly CommandHandlerSettings _commandHandlerSettings;
    private readonly CommandModule _commandModule;
    private readonly IConverter _converter;
    private readonly ILogger<MessageHandlerService> _logger;
    private readonly IServiceProvider _provider;

    public MessageHandlerService(CommandHandlerSettings commandHandlerSettings,
        IConverter converter, ILogger<MessageHandlerService> logger, IServiceProvider provider,
        CommandModule commandModule, Command command)
    {
        _commandHandlerSettings = commandHandlerSettings;
        _converter = converter;
        _logger = logger;
        _provider = provider;
        _commandModule = commandModule;
        _command = command;
    }

    public override Task<bool> OnPlayerTypedMessage(AddonPlayer player, ChatChannel channel, string message)
    {
        if (!message.StartsWith(
                $"{_commandHandlerSettings.CommandRegex.ToLower()}{_command.CommandName.ToLower()}"))
            return Task.FromResult(true);

        if (player is null)
        {
            _logger.LogError("The player is null.");
            return Task.FromResult(true);
        }

        var context = new Context
        {
            Player = player,
            ChatChannel = channel,
            GameServer = player.GameServer,
            ServiceProvider = _provider
        };

        var parametersFromCommand = message.Split(" ").ToList();

        var result = _converter.TryConvertParameters(parametersFromCommand,
            _command,
            context, out var convertedParameters);
        switch (result)
        {
            case Result.Success:
            {
                _commandModule.Context = context;
                try
                {
                    var commandResult =
                        (Task<bool>)_command.MethodInfo.Invoke(_commandModule, convertedParameters.ToArray())!;
                    return Task.FromResult(commandResult.Result);
                }
                catch (Exception e)
                {
                    var errorMessage = e.Message;
                    _logger.LogError(errorMessage, e);
                    player.Message(errorMessage);
                    return Task.FromResult(_commandHandlerSettings.ShowCommandOnChatWhenError);
                }
            }
            case Result.Error:
                player.Message(_commandHandlerSettings.ErrorCallback);
                return Task.FromResult(_commandHandlerSettings.ShowCommandOnChatWhenError);
            case Result.Checker:
                player.Message(_commandHandlerSettings.CheckerCallback);
                return Task.FromResult(_commandHandlerSettings.ShowCommandOnChatWhenChecker);
        }

        return Task.FromResult(_commandHandlerSettings.ShowCommandOnChat);
    }
}