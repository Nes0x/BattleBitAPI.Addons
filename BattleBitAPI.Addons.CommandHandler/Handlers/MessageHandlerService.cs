using System.Reflection;
using BattleBitAPI.Addons.CommandHandler.Common;
using BattleBitAPI.Common;

namespace BattleBitAPI.Addons.CommandHandler.Handlers;

public class MessageHandlerService<TPlayer> where TPlayer : Player
{
    private readonly CommandHandlerSettings _commandHandlerSettings;

    public MessageHandlerService(CommandHandlerSettings commandHandlerSettings)
    {
        _commandHandlerSettings = commandHandlerSettings;
    }

    public Task OnPlayerTypedMessage(TPlayer player, ChatChannel chatChannel, string content,
        Command<TPlayer> command, MethodRepresentation methodRepresentation)
    {
        var property = command.GetType().GetProperty("Context");
        property!.SetValue(command, new Context<TPlayer>
        {
            Player = player,
            ChatChannel = chatChannel
        });
        var parametersFromCommand = content.Split(" ").ToList();
        var parametersFromMethod = methodRepresentation.MethodInfo.GetParameters();
        if (TryConvertParameters(parametersFromCommand, parametersFromMethod, out var convertedParameters))
        {
            if (content.StartsWith(
                    $"{_commandHandlerSettings.CommandRegex.ToLower()}{methodRepresentation.CommandName.ToLower()}"))
                methodRepresentation.MethodInfo.Invoke(command, convertedParameters.ToArray());
        }
        else
        {
            player.Message(_commandHandlerSettings.ErrorCallback);
        }

        return Task.CompletedTask;
    }

    private bool ValidateParameters(List<string> commandParameters, ParameterInfo[] methodParameters)
    {
        commandParameters.RemoveAt(0);
        return commandParameters.Count == methodParameters.Length;
    }

    private bool TryConvertParameters(List<string> commandParameters, ParameterInfo[] methodParameters,
        out List<object> convertedParameters)
    {
        convertedParameters = new List<object>();
        if (!ValidateParameters(commandParameters, methodParameters)) return false;
        for (var i = 0; i < methodParameters.Length; i++)
            try
            {
                convertedParameters.Add(Convert.ChangeType(commandParameters[i],
                    methodParameters[i].ParameterType));
            }
            catch (Exception)
            {
                return false;
            }

        return true;
    }
}