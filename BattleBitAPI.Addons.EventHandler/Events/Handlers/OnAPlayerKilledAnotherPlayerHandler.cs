using System.Numerics;
using BattleBitAPI.Server;

namespace BattleBitAPI.Addons.EventHandler.Events.Handlers;

public abstract class OnAPlayerKilledAnotherPlayerHandler<TPlayer> : EventHandler<TPlayer> where TPlayer : Player
{
    protected OnAPlayerKilledAnotherPlayerHandler(ServerListener<TPlayer> serverListener) : base(serverListener)
    {
    }

    /// <summary>
    ///     Fired when a player kills another player.
    /// </summary>
    /// <remarks>
    ///     Player: The killer player<br />
    ///     Vector3: The position of killer<br />
    ///     Player: The target player that got killed<br />
    ///     Vector3: The target player's position<br />
    ///     string - Tool: The tool user to kill the player<br />
    /// </remarks>
    protected abstract Task HandleAsync(TPlayer arg1, Vector3 arg2, TPlayer arg3, Vector3 arg4, string arg5);

    public override void Subscribe()
    {
        ServerListener.OnAPlayerKilledAnotherPlayer += HandleAsync;
    }

    public override void UnSubscribe()
    {
        ServerListener.OnAPlayerKilledAnotherPlayer -= HandleAsync;
    }
}