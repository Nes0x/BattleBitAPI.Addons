namespace BattleBitAPI.Addons.EventHandler.Common;

[AttributeUsage(AttributeTargets.Method)]
public class EventAttribute : Attribute
{
    public required EventType EventType { get; init; }
}