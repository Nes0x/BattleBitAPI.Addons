using BattleBitAPI.Addons.Common;
using BattleBitAPI.Addons.EventHandler.Common;

namespace BattleBitAPI.Addons.EventHandler.Events;

public abstract class EventPlayer : AddonPlayer
{
    protected readonly EventModule EventModule;
    protected readonly Event Event;

    protected EventPlayer(EventModule eventModule, Event @event)
    {
        EventModule = eventModule;
        Event = @event;
    }
}