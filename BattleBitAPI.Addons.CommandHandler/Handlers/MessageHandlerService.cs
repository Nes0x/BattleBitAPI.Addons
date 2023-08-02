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
        var parametersFromCommand = content.Split(" ").ToList();
        var parametersFromMethod = methodRepresentation.MethodInfo.GetParameters();
        var context = new Context<TPlayer>
        {
            Player = player,
            ChatChannel = chatChannel
        };
        if (!ValidateCheckers(methodRepresentation.MethodInfo.GetCustomAttributes()
                .Where(a => a is CheckerAttribute<TPlayer>), context))
            return Task.CompletedTask;

        if (TryConvertParameters(parametersFromCommand, parametersFromMethod, out var convertedParameters))
        {
            ChangeContext(command.GetType(), command, context);
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

    private bool ValidateCheckers(IEnumerable<Attribute> attributes, Context<TPlayer> context)
    {
        foreach (var attribute in attributes)
        {
            ChangeContext(attribute.GetType(), attribute, context);
            if (((CheckerAttribute<TPlayer>)attribute).RunCommand() == false) return false;
        }

        return true;
    }

    private void ChangeContext(Type type, object obj, Context<TPlayer> context)
    {
        var property = type.GetProperty("Context");
        property!.SetValue(obj, context);
    }

    private bool ValidateParameters(List<string> commandParameters, ParameterInfo[] methodParameters,
        out int finalMethodParameters)
    {
        var methodParametersLength = methodParameters.Length;
        commandParameters.RemoveAt(0);
        foreach (var methodParameter in methodParameters)
            if (methodParameter.HasDefaultValue)
                methodParametersLength--;

        if (commandParameters.Count == methodParameters.Length)
        {
            finalMethodParameters = methodParameters.Length;
            return true;
        }

        if (commandParameters.Count == methodParametersLength)
        {
            finalMethodParameters = methodParametersLength;
            return true;
        }

        finalMethodParameters = -1;
        return false;
    }


    private bool TryConvertParameters(List<string> commandParameters, ParameterInfo[] methodParameters,
        out List<object> convertedParameters)
    {
        convertedParameters = new List<object>();
        if (!ValidateParameters(commandParameters, methodParameters, out var finalMethodParameters)) return false;
        for (var i = 0; i < finalMethodParameters; i++)
            try
            {
                convertedParameters.Add(Convert.ChangeType(commandParameters[i],
                    methodParameters[i].ParameterType));
            }
            catch (Exception e)
            {
                return false;
            }

        if (finalMethodParameters == methodParameters.Length) return true;
        for (var i = finalMethodParameters; i < methodParameters.Length; i++)
        {
            var methodParameter = methodParameters[i];
            if (methodParameter.HasDefaultValue)
                convertedParameters.Add(Convert.ChangeType(methodParameter.DefaultValue,
                    methodParameter.ParameterType)!);
        }

        return true;
    }
}