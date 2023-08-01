using BattleBitAPI.Server;

namespace BattleBitAPI.Addons.EventHandler.Events;

public abstract class EventHandler<TPlayer> : IEventHandler<TPlayer> where TPlayer : Player
{
    protected EventHandler(ServerListener<TPlayer> serverListener)
    {
        ServerListener = serverListener;
    }

    public ServerListener<TPlayer> ServerListener { get; }

    public abstract void Subscribe();

    public abstract void UnSubscribe();
}