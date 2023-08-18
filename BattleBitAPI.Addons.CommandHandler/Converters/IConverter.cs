using BattleBitAPI.Addons.CommandHandler.Common;

namespace BattleBitAPI.Addons.CommandHandler.Converters;

public interface IConverter
{
    Result TryConvertParameters(List<string> commandParameters,
        Command command, Context context,
        out List<object?> convertedParameters);
}