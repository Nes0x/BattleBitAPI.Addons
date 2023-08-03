using System.Reflection;
using BattleBitAPI.Addons.CommandHandler.Common;
using BattleBitAPI.Addons.CommandHandler.Converters.TypeReaders;
using BattleBitAPI.Addons.CommandHandler.Validations;
using Microsoft.Extensions.Logging;

namespace BattleBitAPI.Addons.CommandHandler.Converters;

public class CommandConverter<TPlayer> where TPlayer : Player
{
    private readonly CommandValidator<TPlayer> _commandValidator;
    private readonly ILogger<CommandConverter<TPlayer>> _logger;
    private readonly IEnumerable<TypeReader<TPlayer>> _typeReaders;

    public CommandConverter(CommandValidator<TPlayer> commandValidator, IEnumerable<TypeReader<TPlayer>> typeReaders,
        ILogger<CommandConverter<TPlayer>> logger)
    {
        _commandValidator = commandValidator;
        _typeReaders = typeReaders;
        _logger = logger;
    }

    public Result TryConvertParameters(List<string> commandParameters, ParameterInfo[] methodParameters,
        MethodRepresentation methodRepresentation, Context<TPlayer> context,
        out List<object> convertedParameters)
    {
        convertedParameters = new List<object>();
        if (!_commandValidator.ValidateCheckers(methodRepresentation.MethodInfo.GetCustomAttributes()
                .Where(a => a is CheckerAttribute<TPlayer>), context))
            return Result.Checker;

        if (!_commandValidator.ValidateParametersCount(commandParameters, methodParameters,
                out var finalMethodParameters))
            return Result.Error;

        for (var i = 0; i < finalMethodParameters; i++)
            if (TryConvertParameter(commandParameters[i], methodParameters[i].ParameterType, context,
                    out var convertedType))
                convertedParameters.Add(convertedType);

        var methodLength = methodParameters.Length;
        if (finalMethodParameters == methodLength && finalMethodParameters == convertedParameters.Count)
            return Result.Success;
        for (var i = finalMethodParameters; i < methodLength; i++)
        {
            var methodParameter = methodParameters[i];
            if (methodParameter.HasDefaultValue && TryConvertParameter(methodParameter.DefaultValue!,
                    methodParameter.ParameterType, context, out var convertedType))
                convertedParameters.Add(convertedType);
        }

        return methodLength == convertedParameters.Count ? Result.Success : Result.Error;
    }

    private bool TryConvertParameter(object value, Type type, Context<TPlayer> context, out object convertedType)
    {
        try
        {
            convertedType = Convert.ChangeType(value, type);
            return true;
        }
        catch (InvalidCastException e)
        {
            var typeReader = _typeReaders.FirstOrDefault(tr => tr.Type == type);
            if (typeReader is null)
            {
                _logger.LogError($"Cannot convert {type.Name} type. Try add custom type reader.", e);
            }
            else
            {
                context.ChangeContext(typeReader);
                try
                {
                    convertedType = typeReader.ChangeType(value);
                    return true;
                }
                catch (Exception)
                {
                }
            }
        }
        catch (Exception)
        {
        }

        convertedType = null;
        return false;
    }
}