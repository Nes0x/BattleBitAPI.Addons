using System.Text;
using BattleBitAPI.Addons.CommandHandler.Common;
using BattleBitAPI.Addons.CommandHandler.Handlers;

namespace BattleBitAPI.Addons.CommandHandler.Modules;

public class HelpModule<TPlayer> : CommandModule<TPlayer> where TPlayer : Player 
{
    private readonly CommandHandlerSettings _commandHandlerSettings;
    
    public HelpModule(CommandHandlerSettings commandHandlerSettings)
    {
        _commandHandlerSettings = commandHandlerSettings;
    }
    
    
    [Command(Name = "help", Description = "Show all available commands.")]
    public Task HandleHelpAsync()
    {
        var stringBuilder = new StringBuilder();
        foreach (var commandModule in CommandHandlerActivatorService<TPlayer>.Commands)
        {
            commandModule.Commands.ForEach(command =>
            {
                var formattedParameters = new StringBuilder();
                
                foreach (var commandParameter in command.Parameters)
                {
                    var formattedParameter = commandParameter.HasDefaultValue
                        ? $"{commandParameter.Name} Has default value : {commandParameter.DefaultValue}" : commandParameter.Name;
                    formattedParameters.Append(formattedParameter).Append(" ");
                }
                
                stringBuilder
                    .Append(_commandHandlerSettings.CommandRegex)
                    .Append(command.CommandName)
                    .Append(formattedParameters)
                    .Append(" - ")
                    .Append(command.CommandDescription)
                    .Append('\n');
            });
          
        }
        
        Context.Player.Message(stringBuilder.ToString());

        return Task.CompletedTask;
    }
}