using System.Reflection;
using BattleBitAPI.Addons.CommandHandler.Common;

namespace BattleBitAPI.Addons.CommandHandler.Converters;

public interface IConverter<TPlayer> where TPlayer : Player
{
    Result TryConvertParameters(List<string> commandParameters, ParameterInfo[] methodParameters,
        MethodRepresentation methodRepresentation, Context<TPlayer> context,
        out List<object> convertedParameters);
}