using System.Net;
using BattleBitAPI.Addons.Common;
using BattleBitAPI.Server;

namespace BattleBitAPI.Addons.EventHandler.Events;

public class OnGameServerConnectingArgs
{
    public required IPAddress IpAddress { get; init; }
}

public class OnValidateGameServerTokenArgs
{
    public required IPAddress IpAddress { get; init; }
    public required ulong Port { get; init; }
    public required string Token { get; init; }
}

public class OnGameServerConnectedArgs
{
    public required GameServer<AddonPlayer> GameServer { get; init; }
}

public class OnGameServerDisconnectedArgs
{
    public required GameServer<AddonPlayer> GameServer { get; init; }
}