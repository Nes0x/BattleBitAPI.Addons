using BattleBitAPI.Addons.CommandHandler.Common;
using BattleBitAPI.Common;

namespace BattleBitAPI.Addons.CommandHandler.Handlers;

public interface IMessageHandler<TPlayer> where TPlayer : Player
{
    Task OnPlayerTypedMessage(TPlayer player, ChatChannel chatChannel, string content,
        CommandModule<TPlayer> commandModule, Command command);
}