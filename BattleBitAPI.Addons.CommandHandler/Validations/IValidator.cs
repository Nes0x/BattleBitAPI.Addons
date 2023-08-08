using BattleBitAPI.Addons.CommandHandler.Common;

namespace BattleBitAPI.Addons.CommandHandler.Validations;

public interface IValidator<TPlayer> where TPlayer : Player
{
    bool ValidateParametersCount(List<string> commandParameters, Command command,
        out int finalMethodParameters);

    bool ValidateCheckers(IEnumerable<Attribute> attributes, Context<TPlayer> context);
    bool ValidateUniqueCommand(Command command, List<Command> commands);
}