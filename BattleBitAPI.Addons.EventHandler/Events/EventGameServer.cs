using BattleBitAPI.Addons.Common;
using BattleBitAPI.Addons.EventHandler.Common;

namespace BattleBitAPI.Addons.EventHandler.Events;

public abstract class EventGameServer : AddonGameServer
{
    protected readonly EventModule EventModule;
    protected readonly Event Event;

    protected EventGameServer(EventModule eventModule, Event @event)
    {
        EventModule = eventModule;
        Event = @event;
    }
}