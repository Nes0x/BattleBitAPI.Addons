using System.Reflection;
using BattleBitAPI.Addons.CommandHandler.Common;
using Microsoft.Extensions.Logging;

namespace BattleBitAPI.Addons.CommandHandler.Validations;

public class CommandValidator<TPlayer> where TPlayer : Player
{
    private readonly ILogger<CommandValidator<TPlayer>> _logger;

    public CommandValidator(ILogger<CommandValidator<TPlayer>> logger)
    {
        _logger = logger;
    }

    public bool ValidateParametersCount(List<string> commandParameters, ParameterInfo[] methodParameters,
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

    public bool ValidateCheckers(IEnumerable<Attribute> attributes, Context<TPlayer> context)
    {
        foreach (var attribute in attributes)
            try
            {
                context.ChangeContext(attribute);
                if (!((CheckerAttribute<TPlayer>)attribute).RunCommand()) return false;
            }
            catch (Exception e)
            {
                _logger.LogError($"Checker {e.TargetSite.DeclaringType.Name} threw an exception.", e);
            }

        return true;
    }
}