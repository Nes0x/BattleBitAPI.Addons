using System.Reflection;
using BattleBitAPI.Addons.Common;
using BattleBitAPI.Addons.EventHandler.Common;
using BattleBitAPI.Addons.EventHandler.Events;
using BattleBitAPI.Common;
using BattleBitAPI.Server;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace BattleBitAPI.Addons.EventHandler;

public class EventHandlerActivatorService : IHostedService
{
    private readonly IEnumerable<EventModule> _eventModules;
    private readonly ServerListener<AddonPlayer, AddonGameServer> _serverListener;
    private readonly ILogger<EventHandlerActivatorService> _logger;

    public EventHandlerActivatorService(IEnumerable<EventModule> eventModules, ServerListener<AddonPlayer, AddonGameServer> serverListener, ILogger<EventHandlerActivatorService> logger)
    {
        _eventModules = eventModules;
        _serverListener = serverListener;
        _logger = logger;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        ModifyEventModules();
        try
        {
            // RegisterEvents();
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
        {
            switch (@event.EventType)
            {
                case EventType.OnPlayerConnected:
                    _serverListener.OnCreatingGameServerInstance += () => new OnPlayerConnectedEvent(eventModule, @event);
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
                .Select(m => new Event { MethodInfo = m, EventType = m.GetCustomAttribute<EventAttribute>()!.EventType })
                .ToList();
            eventModule.Events = events;
        }
    }
}