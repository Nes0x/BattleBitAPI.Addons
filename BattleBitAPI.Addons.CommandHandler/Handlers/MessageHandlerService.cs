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

    public override Task<bool> OnPlayerTypedMessage(AddonPlayer player, ChatChannel channel, string content)
    {
        if (!content.StartsWith(
                $"{_commandHandlerSettings.CommandRegex.ToLower()}{_command.CommandName.ToLower()}"))
            return Task.FromResult(true);

        if (player is null)
        {
            _logger.LogError("The player is null.");
            return Task.FromResult(false);
        }

        var context = new Context
        {
            Player = player,
            ChatChannel = channel,
            GameServer = player.GameServer,
            ServiceProvider = _provider
        };

        var parametersFromCommand = content.Split(" ").ToList();

        var result = _converter.TryConvertParameters(parametersFromCommand,
            _command,
            context, out var convertedParameters);
        string message = null;
        switch (result)
        {
            case Result.Success:
            {
                _commandModule.Context = context;
                try
                {
                    _command.MethodInfo.Invoke(_commandModule, convertedParameters.ToArray());
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

        return Task.FromResult(true);
    }
}