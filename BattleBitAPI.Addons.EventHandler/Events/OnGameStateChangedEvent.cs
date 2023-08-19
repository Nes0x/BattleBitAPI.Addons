using BattleBitAPI.Addons.Common;
using BattleBitAPI.Addons.EventHandler.Common;
using BattleBitAPI.Common;

namespace BattleBitAPI.Addons.EventHandler.Events;

public class OnGameStateChangedEvent : EventGameServer
{
    public OnGameStateChangedEvent(EventModule eventModule, Event @event) : base(eventModule, @event)
    {
    }

    public override Task OnGameStateChanged(GameState oldState, GameState newState)
    {
        {
            return (Task)Event.MethodInfo.Invoke(EventModule, new[]
            {
                new OnGameStateChangedArgs()
                {
                    OldGameState = oldState,
                    NewGameState = newState,
                    GameServer = this
                }
        
            });
        }
    }


}

public class OnGameStateChangedArgs : IGameServerArgs
{
    public required GameState OldGameState { get; init; }
    public required GameState NewGameState { get; init; }
    public required AddonGameServer GameServer { get; init; }
}