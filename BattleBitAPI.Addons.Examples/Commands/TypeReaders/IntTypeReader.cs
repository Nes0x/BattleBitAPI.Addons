using BattleBitAPI.Addons.CommandHandler.Converters.TypeReaders;

namespace BattleBitAPI.Addons.Examples.Commands.TypeReaders;

public class IntTypeReader : TypeReader<Player>
{
    public override object ChangeType(object obj)
    {
        return (int)obj;
    }
}