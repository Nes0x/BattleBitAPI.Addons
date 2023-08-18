using BattleBitAPI.Addons.CommandHandler.Common;

namespace BattleBitAPI.Addons.CommandHandler.Validations;

public interface IValidator
{
    bool ValidateParametersCount(List<string> commandParameters, Command command,
        out int finalMethodParameters);

    bool ValidateCheckers(IEnumerable<Attribute> attributes, Context context);
    bool ValidateUniqueCommand(Command command, List<Command> commands);
}