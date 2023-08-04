namespace BattleBitAPI.Addons.CommandHandler.Converters.TypeReaders;

public class PlayerTypeReader : TypeReader<Player>
{
    public override Player ChangeType(object obj)
    {
        return Context.GameServer.GetAllPlayers().FirstOrDefault(p => p.Name == obj.ToString());
    }

    public PlayerTypeReader() : base(typeof(Player))
    {
    }
}