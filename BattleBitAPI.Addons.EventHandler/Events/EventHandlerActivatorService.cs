using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace BattleBitAPI.Addons.EventHandler.Events;

public class EventHandlerActivatorService<TPlayer> : IHostedService where TPlayer : Player
{
    private readonly IEnumerable<IEventHandler<TPlayer>> _handlers;
    private readonly ILogger<EventHandlerActivatorService<TPlayer>> _logger;

    public EventHandlerActivatorService(IEnumerable<IEventHandler<TPlayer>> handlers,
        ILogger<EventHandlerActivatorService<TPlayer>> logger)
    {
        _handlers = handlers;
        _logger = logger;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        foreach (var handler in _handlers)
            try
            {
                handler.Subscribe();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message, e);
            }

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        foreach (var handler in _handlers)
            handler.UnSubscribe();
        return Task.CompletedTask;
    }
}