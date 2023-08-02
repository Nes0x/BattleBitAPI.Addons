using Microsoft.Extensions.Hosting;

namespace BattleBitAPI.Addons.EventHandler.Events;

public class EventHandlerActivatorService<TPlayer> : IHostedService where TPlayer : Player
{
    private readonly IEnumerable<IEventHandler<TPlayer>> _handlers;

    public EventHandlerActivatorService(IEnumerable<IEventHandler<TPlayer>> handlers)
    {
        _handlers = handlers;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        foreach (var handler in _handlers)
            handler.Subscribe();
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        foreach (var handler in _handlers)
            handler.UnSubscribe();
        return Task.CompletedTask;
    }
}