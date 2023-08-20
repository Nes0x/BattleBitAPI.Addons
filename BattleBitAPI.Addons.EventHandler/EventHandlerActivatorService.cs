using System.Net;
using System.Reflection;
using BattleBitAPI.Addons.Common;
using BattleBitAPI.Addons.EventHandler.Common;
using BattleBitAPI.Addons.EventHandler.Events;
using BattleBitAPI.Server;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace BattleBitAPI.Addons.EventHandler;

public class EventHandlerActivatorService : IHostedService
{
    private readonly List<Func<IPAddress, ushort, AddonGameServer>> _eventGameHandlers;
    private readonly IEnumerable<EventModule> _eventModules;
    private readonly ILogger<EventHandlerActivatorService> _logger;
    private readonly ServerListener<AddonPlayer, AddonGameServer> _serverListener;

    public EventHandlerActivatorService(IEnumerable<EventModule> eventModules,
        ServerListener<AddonPlayer, AddonGameServer> serverListener, ILogger<EventHandlerActivatorService> logger)
    {
        _eventModules = eventModules;
        _eventGameHandlers = new List<Func<IPAddress, ushort, AddonGameServer>>();
        _serverListener = serverListener;
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
            _logger.LogError(
                $"Your event implementation threw an exception. Class name {e.InnerException.TargetSite.DeclaringType.Name}.",
                e);
        }

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        foreach (var handler in _eventGameHandlers) _serverListener.OnCreatingGameServerInstance -= handler;
        _eventGameHandlers.Clear();
        return Task.CompletedTask;
    }

    private void RegisterEvents()
    {
        foreach (var eventModule in _eventModules)
        foreach (var @event in eventModule.Events)
            try
            {
                var eventGameServer = (AddonGameServer)Activator.CreateInstance(
                    Type.GetType($"BattleBitAPI.Addons.EventHandler.Events.{@event.EventType.ToString()}Event"),
                    eventModule, @event);
                if (eventGameServer is null) continue;
                Func<IPAddress, ushort, AddonGameServer> handler = (_, _) => eventGameServer;
                _serverListener.OnCreatingGameServerInstance += handler;
                _eventGameHandlers.Add(handler!);
            }
            catch (Exception)
            {
                switch (@event.EventType)
                {
                    case EventType.OnGameServerConnecting:
                        _serverListener.OnGameServerConnecting += address =>
                        {
                            return (Task<bool>)@event.MethodInfo.Invoke(eventModule, new[]
                            {
                                new OnGameServerConnectingArgs
                                {
                                    IpAddress = address
                                }
                            });
                        };
                        break;
                    case EventType.OnValidateGameServerToken:
                        _serverListener.OnValidateGameServerToken += (address, port, token) =>
                        {
                            return (Task<bool>)@event.MethodInfo.Invoke(eventModule, new[]
                            {
                                new OnValidateGameServerTokenArgs
                                {
                                    IpAddress = address,
                                    Port = port,
                                    Token = token
                                }
                            });
                        };
                        break;
                    case EventType.OnGameServerConnected:
                        _serverListener.OnGameServerConnected += server =>
                        {
                            return (Task)@event.MethodInfo.Invoke(eventModule, new[]
                            {
                                new OnGameServerConnectedArgs
                                {
                                    GameServer = server
                                }
                            });
                        };
                        break;
                    case EventType.OnGameServerDisconnected:
                        _serverListener.OnGameServerDisconnected += server =>
                        {
                            return (Task)@event.MethodInfo.Invoke(eventModule, new[]
                            {
                                new OnGameServerDisconnectedArgs
                                {
                                    GameServer = server
                                }
                            });
                        };
                        break;
                }
            }
    }

    private void ModifyEventModules()
    {
        foreach (var eventModule in _eventModules)
        {
            var events = eventModule.GetType().GetMethods()
                .Where(m => m.GetCustomAttributes(typeof(EventAttribute), false).Length > 0)
                .Select(m => new Event
                    { MethodInfo = m, EventType = m.GetCustomAttribute<EventAttribute>()!.EventType })
                .ToList();
            eventModule.Events = events;
        }
    }
}