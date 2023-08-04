using BattleBitAPI.Common;
using BattleBitAPI.Server;

namespace BattleBitAPI.Addons.CommandHandler.Common;

public class Context<TPlayer> : IContext<TPlayer> where TPlayer : Player
{
    public required TPlayer Player { get; init; }
    public required ChatChannel ChatChannel { get; init; }
    public required GameServer GameServer { get; init; }

    internal void ChangeContext(object obj)
    {
        var property = obj.GetType().GetProperty("Context");
        property!.SetValue(obj, this);
    }
}