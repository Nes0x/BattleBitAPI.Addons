using BattleBitAPI.Common;
using BattleBitAPI.Server;

namespace BattleBitAPI.Addons.EventHandler.Events.Handlers;

public abstract class OnPlayerTypedMessageHandler<TPlayer> : EventHandler<TPlayer> where TPlayer : Player
{
    protected OnPlayerTypedMessageHandler(ServerListener<TPlayer> serverListener) : base(serverListener)
    {
    }

    /// <summary>
    ///     Fired when a player types a message to text chat.<br />
    /// </summary>
    /// <remarks>
    ///     Player: The player that typed the message <br />
    ///     ChatChannel: The channel the message was sent <br />
    ///     string - Message: The message<br />
    /// </remarks>
    protected abstract Task HandleAsync(TPlayer arg1, ChatChannel arg2, string arg3);

    public override void Subscribe()
    {
        ServerListener.OnPlayerTypedMessage += HandleAsync;
    }

    public override void UnSubscribe()
    {
        ServerListener.OnPlayerTypedMessage -= HandleAsync;
    }
}