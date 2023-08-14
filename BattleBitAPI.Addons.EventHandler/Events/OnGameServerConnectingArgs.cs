using System.Net;

namespace BattleBitAPI.Addons.EventHandler.Events;

public class OnGameServerConnectingArgs
{
    public IPAddress IPAddress { get; internal init; }
}