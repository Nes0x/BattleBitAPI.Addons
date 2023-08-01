using BattleBitAPI.Addons.EventHandler.Events.Handlers;
using BattleBitAPI.Server;

namespace BattleBitAPI.Addons.Examples.Handlers;

public class OnPlayerDisconnected : OnPlayerDisconnectedHandler<Player>
{
    public OnPlayerDisconnected(ServerListener<Player> serverListener) : base(serverListener)
    {
    }

    protected override Task HandleAsync(Player arg)
    {
        //do anything
        throw new NotImplementedException();
    }
}