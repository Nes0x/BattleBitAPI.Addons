﻿using System.Reflection;
using BattleBitAPI.Addons.CommandHandler.Common;
using BattleBitAPI.Addons.CommandHandler.Converters.TypeReaders;
using BattleBitAPI.Addons.CommandHandler.Validations;
using Microsoft.Extensions.Logging;

namespace BattleBitAPI.Addons.CommandHandler.Converters;

public class CommandConverter : IConverter
{
    private readonly ILogger<CommandConverter> _logger;
    private readonly IEnumerable<TypeReader> _typeReaders;
    private readonly IValidator _validator;

    public CommandConverter(IValidator validator, IEnumerable<TypeReader> typeReaders,
        ILogger<CommandConverter> logger)
    {
        _validator = validator;
        _typeReaders = typeReaders;
        _logger = logger;
    }

    public Result TryConvertParameters(List<string> commandParameters,
        Command command, Context context,
        out List<object?> convertedParameters)
    {
        convertedParameters = new List<object?>();
        if (!_validator.ValidateCheckers(GetCheckers(command.MethodInfo), context))
            return Result.Checker;

        var methodParameters = command.Parameters;

        if (!_validator.ValidateParametersCount(commandParameters, command,
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

    private IEnumerable<Attribute> GetCheckers(MethodInfo methodInfo)
    {
        var attributes = new HashSet<Attribute>();
        var attributesFromClass = methodInfo.DeclaringType.GetCustomAttributes()
            .Where(a => a is CheckerAttribute);
        var attributesFromMethod = methodInfo.GetCustomAttributes()
            .Where(a => a is CheckerAttribute);
        attributes.UnionWith(attributesFromClass);
        attributes.UnionWith(attributesFromMethod);
        return attributes;
    }

    private bool TryConvertParameter(object value, Type type, Context context, out object? convertedType)
    {
        try
        {
            convertedType = Convert.ChangeType(value, type);
            return true;
        }
        catch (InvalidCastException)
        {
            var typeReaders = _typeReaders.Where(tr => tr.Type == type).ToArray();
            if (typeReaders.Length == 0)
                _logger.LogError("Cannot convert {TypeName} type. Try add custom type reader", type.Name);
            else
                foreach (var typeReader in typeReaders)
                {
                    typeReader.Context = context;
                    try
                    {
                        convertedType = typeReader.ChangeType(value);
                        return true;
                    }
                    catch (Exception e)
                    {
                        _logger.LogError("TypeReader {Name} threw an exception",
                            e.TargetSite.DeclaringType.Name);
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