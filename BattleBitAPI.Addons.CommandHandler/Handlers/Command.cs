namespace BattleBitAPI.Addons.CommandHandler.Handlers;

public abstract class Command<TPlayer> where TPlayer : Player
{
    public Context<TPlayer> Context { get; set; }

    public Task HandleAsync(Context<TPlayer> author)
    {
        throw new NotImplementedException();
    }
}