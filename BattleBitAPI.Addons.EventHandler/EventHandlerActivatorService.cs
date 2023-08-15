using System.Reflection;
using BattleBitAPI.Addons.EventHandler.Common;
using BattleBitAPI.Addons.EventHandler.Events;
using BattleBitAPI.Common;
using BattleBitAPI.Server;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace BattleBitAPI.Addons.EventHandler;

public class EventHandlerActivatorService<TPlayer> : IHostedService where TPlayer : Player
{
    private readonly IEnumerable<EventModule<TPlayer>> _eventModules;
    private readonly ServerListener<TPlayer> _serverListener;
    private readonly ILogger<EventHandlerActivatorService<TPlayer>> _logger;

    public EventHandlerActivatorService(ServerListener<TPlayer> serverListener,
        IEnumerable<EventModule<TPlayer>> eventModules, ILogger<EventHandlerActivatorService<TPlayer>> logger)
    {
        _serverListener = serverListener;
        _eventModules = eventModules;
        _logger = logger;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        ModifyEventModules();
        try
        {
            RegisterEvents();
        }
        catch (Exception e)
        {
            _logger.LogError($"Your event implementation threw an exception. Class name {e.InnerException.TargetSite.DeclaringType.Name}.", e);
        }
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    private void RegisterEvents()
    {
        foreach (var eventModule in _eventModules)
        foreach (var @event in eventModule.Events)
            switch (@event.EventType)
            {
                case EventType.OnPlayerConnected:
                    _serverListener.OnPlayerConnected += player =>
                    {
                        return (Task)@event.MethodInfo.Invoke(eventModule, new[]
                        {
                            new OnPlayerConnectedArgs<TPlayer>
                            {
                                Player = player
                            }
                        });
                    };
                    break;
                case EventType.OnPlayerDied:
                    _serverListener.OnPlayerDied += player =>
                    {
                        return (Task)@event.MethodInfo.Invoke(eventModule, new[]
                        {
                            new OnPlayerDiedArgs<TPlayer>
                            {
                                Player = player
                            }
                        });
                    };
                    break;
                case EventType.OnAPlayerKilledAnotherPlayer:
                    _serverListener.OnAPlayerKilledAnotherPlayer +=
                        (killer, killerPosition, victim, victimPosition, tool) =>
                        {
                            return (Task)@event.MethodInfo.Invoke(eventModule, new[]
                            {
                                new OnAPlayerKilledAnotherPlayerArgs<TPlayer>
                                {
                                    Killer = killer,
                                    KillerPosition = killerPosition,
                                    Victim = victim,
                                    VictimPosition = victimPosition
                                }
                            });
                        };
                    break;
                case EventType.OnGameServerConnected:
                    _serverListener.OnGameServerConnected += gameServer =>
                    {
                        return (Task)@event.MethodInfo.Invoke(eventModule, new[]
                        {
                            new OnGameServerConnectedArgs
                            {
                                GameServer = gameServer
                            }
                        });
                    };
                    break;
                case EventType.OnGameServerConnecting:
                    _serverListener.OnGameServerConnecting += ipAddress =>
                    {
                        return (Task<bool>)@event.MethodInfo.Invoke(eventModule, new[]
                        {
                            new OnGameServerConnectingArgs
                            {
                                IPAddress = ipAddress
                            }
                        });
                    };
                    break;
                case EventType.OnGameServerDisconnected:
                    _serverListener.OnGameServerDisconnected += gameServer =>
                    {
                        return (Task)@event.MethodInfo.Invoke(eventModule, new[]
                        {
                            new OnGameServerDisconnectedArgs
                            {
                                GameServer = gameServer
                            }
                        });
                    };
                    break;
                case EventType.OnGameServerReconnected:
                    _serverListener.OnGameServerReconnected += gameServer =>
                    {
                        return (Task)@event.MethodInfo.Invoke(eventModule, new[]
                        {
                            new OnGameServerReconnectedArgs
                            {
                                GameServer = gameServer
                            }
                        });
                    };
                    break;
                case EventType.OnGameServerTick:
                    _serverListener.OnGameServerTick += gameServer =>
                    {
                        return (Task)@event.MethodInfo.Invoke(eventModule, new[]
                        {
                            new OnGameServerTickArgs
                            {
                                GameServer = gameServer
                            }
                        });
                    };
                    break;
                case EventType.OnGetPlayerStats:
                    _serverListener.OnGetPlayerStats += (steamId, playerStats) =>
                    {
                        return (Task<PlayerStats>)@event.MethodInfo.Invoke(eventModule, new[]
                        {
                            new OnGetPlayerStatsArgs
                            {
                                SteamId = steamId,
                                PlayerStats = playerStats
                            }
                        });
                    };
                    break;
                case EventType.OnPlayerChangedRole:
                    _serverListener.OnPlayerChangedRole += (player, gameRole) =>
                    {
                        return (Task)@event.MethodInfo.Invoke(eventModule, new[]
                        {
                            new OnPlayerChangedRoleArgs<TPlayer>
                            {
                                Player = player,
                                GameRole = gameRole
                            }
                        });
                    };
                    break;
                case EventType.OnPlayerChangedTeam:
                    _serverListener.OnPlayerChangedTeam += (player, team) =>
                    {
                        return (Task)@event.MethodInfo.Invoke(eventModule, new[]
                        {
                            new OnPlayerChangedTeamArgs<TPlayer>
                            {
                                Player = player,
                                Team = team
                            }
                        });
                    };
                    break;
                case EventType.OnPlayerDisconnected:
                    _serverListener.OnPlayerDisconnected += player =>
                    {
                        return (Task)@event.MethodInfo.Invoke(eventModule, new[]
                        {
                            new OnPlayerDisconnectedArgs<TPlayer>
                            {
                                Player = player
                            }
                        });
                    };
                    break;
                case EventType.OnPlayerJoinedASquad:
                    _serverListener.OnPlayerJoinedASquad += (player, squads) =>
                    {
                        return (Task)@event.MethodInfo.Invoke(eventModule, new[]
                        {
                            new OnPlayerJoinedASquadArgs<TPlayer>
                            {
                                Player = player,
                                Squads = squads
                            }
                        });
                    };
                    break;
                case EventType.OnPlayerLeftSquad:
                    _serverListener.OnPlayerLeftSquad += (player, squads) =>
                    {
                        return (Task)@event.MethodInfo.Invoke(eventModule, new[]
                        {
                            new OnPlayerLeftSquadArgs<TPlayer>
                            {
                                Player = player,
                                Squads = squads
                            }
                        });
                    };
                    break;
                case EventType.OnPlayerReported:
                    _serverListener.OnPlayerReported += (reporter, reported, reportReason, details) =>
                    {
                        return (Task)@event.MethodInfo.Invoke(eventModule, new[]
                        {
                            new OnPlayerReportedArgs<TPlayer>
                            {
                                Reporter = reporter,
                                Reported = reported,
                                ReportReason = reportReason,
                                Details = details
                            }
                        });
                    };
                    break;
                case EventType.OnPlayerRequestingToChangeRole:
                    _serverListener.OnPlayerRequestingToChangeRole += (player, gameRole) =>
                    {
                        return (Task<bool>)@event.MethodInfo.Invoke(eventModule, new[]
                        {
                            new OnPlayerRequestingToChangeRoleArgs<TPlayer>
                            {
                                Player = player,
                                GameRole = gameRole
                            }
                        });
                    };
                    break;
                case EventType.OnPlayerSpawned:
                    _serverListener.OnPlayerSpawned += player =>
                    {
                        return (Task)@event.MethodInfo.Invoke(eventModule, new[]
                        {
                            new OnPlayerSpawnedArgs<TPlayer>
                            {
                                Player = player
                            }
                        });
                    };
                    break;
                case EventType.OnPlayerSpawning:
                    _serverListener.OnPlayerSpawning += (player, playerSpawnRequest) =>
                    {
                        return (Task<PlayerSpawnRequest>)@event.MethodInfo.Invoke(eventModule, new[]
                        {
                            new OnPlayerSpawningArgs<TPlayer>
                            {
                                Player = player,
                                PlayerSpawnRequest = playerSpawnRequest
                            }
                        });
                    };
                    break;
                case EventType.OnPlayerTypedMessage:
                    _serverListener.OnPlayerTypedMessage += (player, chatChannel, content) =>
                    {
                        return (Task)@event.MethodInfo.Invoke(eventModule, new[]
                        {
                            new OnPlayerTypedMessageArgs<TPlayer>
                            {
                                Player = player,
                                ChatChannel = chatChannel,
                                Content = content
                            }
                        });
                    };
                    break;
                case EventType.OnSavePlayerStats:
                    _serverListener.OnSavePlayerStats += (steamId, playerStats) =>
                    {
                        return (Task)@event.MethodInfo.Invoke(eventModule, new[]
                        {
                            new OnSavePlayerStatsArgs
                            {
                                SteamId = steamId,
                                PlayerStats = playerStats
                            }
                        });
                    };
                    break;
            }
    }
 
    private void ModifyEventModules()
    {
        foreach (var eventModule in _eventModules)
        {
            var events = eventModule.GetType().GetMethods()
                .Where(m => m.GetCustomAttributes(typeof(EventAttribute), false).Length > 0)
                .Select(m => new Event { MethodInfo = m, EventType = m.GetCustomAttribute<EventAttribute>()!.EventType })
                .ToList();
            eventModule.Events = events;
        }
    }
}