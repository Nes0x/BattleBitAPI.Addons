using BattleBitAPI.Addons.CommandHandler.Common;
using Microsoft.Extensions.Logging;

namespace BattleBitAPI.Addons.CommandHandler.Validations;

public class CommandValidator : IValidator
{
    private readonly ILogger<CommandValidator> _logger;

    public CommandValidator(ILogger<CommandValidator> logger)
    {
        _logger = logger;
    }

    public bool ValidateParametersCount(List<string> commandParameters, Command command,
        out int finalMethodParameters)
    {
        var methodParameters = command.Parameters;
        var methodParametersLength = methodParameters.Length;

        for (var i = 0; i < command.RemoveParameters; i++) commandParameters.RemoveAt(0);

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

    public bool ValidateCheckers(IEnumerable<Attribute> attributes, Context context)
    {
        foreach (var attribute in attributes.Select(a => (CheckerAttribute)a))
            try
            {
                attribute.Context = context;
                if (!attribute.RunCommand()) return false;
            }
            catch (Exception e)
            {
                _logger.LogError("Checker {DeclaringTypeName} threw an exception", e.TargetSite.DeclaringType.Name);
                return false;
            }

        return true;
    }

    public bool ValidateUniqueCommand(Command commandToCheck, List<Command> commands)
    {
        if (commands.Count == 0) return true;
        foreach (var command in commands)
            if (commandToCheck.CommandName == command.CommandName &&
                commandToCheck.Parameters.Length == command.Parameters.Length &&
                !commandToCheck.Parameters.Except(command.Parameters).Any())
            {
                _logger.LogError("You cannot have more commands with same name and parameters count with same types. Currently registered is {MethodInfoName}", command.MethodInfo.Name);
                return false;
            }


        return true;
    }
}