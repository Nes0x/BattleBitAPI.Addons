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
    
    
    [Command(Name = "help")]
    public Task HandleHelpAsync()
    {
        var stringBuilder = new StringBuilder();
        foreach (var commandModule in CommandHandlerActivatorService<TPlayer>.CommandModules)
        {
            commandModule.Commands.ForEach(command =>
            {
                var formattedParameters = new StringBuilder();
                
                foreach (var commandParameter in command.Parameters)
                {
                    var formattedParameter = commandParameter.HasDefaultValue
                        ? $"{commandParameter.Name} Has default value : {commandParameter.DefaultValue}({commandParameter.ParameterType.Name})" : $"{commandParameter.Name}({commandParameter.ParameterType.Name})" ;
                    formattedParameters.Append(formattedParameter).Append(" ");
                }

                stringBuilder
                    .Append(_commandHandlerSettings.CommandRegex)
                    .Append(command.CommandName);
                   

                if (!string.IsNullOrWhiteSpace(formattedParameters.ToString()))
                {
                    stringBuilder
                        .Append(" ")
                        .Append(formattedParameters);
                }

                stringBuilder.Append(" ");
                
                if (!string.IsNullOrWhiteSpace(command.CommandDescription))
                {
                    stringBuilder.Append("- ")
                        .Append(command.CommandDescription);
                }

                stringBuilder.Append('\n');
            });
          
        }

        Context.Player.Message(stringBuilder.ToString());
        return Task.CompletedTask;
    }
}
