using BattleBitAPI.Server;

namespace BattleBitAPI.Addons.EventHandler.Events;

public interface IEventHandler<TPlayer> where TPlayer : Player
{
    public ServerListener<TPlayer> ServerListener { get; }

    public void Subscribe();

    public void UnSubscribe();
}