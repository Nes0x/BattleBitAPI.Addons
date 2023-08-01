using BattleBitAPI.Addons.EventHandler.Events.Handlers;
using BattleBitAPI.Server;

namespace BattleBitAPI.Addons.Examples.Handlers;

public class OnPlayerConnected : OnPlayerConnectedHandler<Player>
{
    public OnPlayerConnected(ServerListener<Player> serverListener) : base(serverListener)
    {
    }

    protected override Task HandleAsync(Player arg)
    {
        //do anything
        throw new NotImplementedException();
    }
}