using Microsoft.Extensions.Hosting;

namespace BattleBitAPI.Addons.EventHandler.Events;

public class EventHandlerActivatorService<TPlayer> : IHostedService where TPlayer : Player
{
    private readonly IEnumerable<IEventHandler<TPlayer>> _handlers;

    public EventHandlerActivatorService(IEnumerable<IEventHandler<TPlayer>> handlers)
    {
        _handlers = handlers;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        Subscribe();
        await Task.CompletedTask;
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        Unsubscribe();
        await Task.CompletedTask;
    }

    private void Subscribe()
    {
        foreach (var handler in _handlers)
            handler.Subscribe();
    }

    private void Unsubscribe()
    {
        foreach (var handler in _handlers)
            handler.UnSubscribe();
    }
}