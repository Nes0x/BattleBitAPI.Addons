namespace BattleBitAPI.Addons.CommandHandler.Converters.TypeReaders;

public class PlayerSteamIdTypeReader : TypeReader<Player>
{
    public PlayerSteamIdTypeReader() : base(typeof(Player))
    {
    }

    public override Player ChangeType(object obj)
    {
        return Context.GameServer.GetAllPlayers().FirstOrDefault(p => p.SteamID == ulong.Parse(obj.ToString()));
    }
}