using System.Numerics;

namespace BattleBitAPI.Addons.EventHandler.Events;

public class OnAPlayerKilledAnotherPlayerArgs<TPlayer> where TPlayer : Player
{
    public TPlayer Killer { get; internal init; }
    public Vector3 KillerPosition { get; internal init; }
    public TPlayer Victim { get; internal init; }
    public Vector3 VictimPosition { get; internal init; }
    public string Tool { get; internal init; }
}