using System.Reflection;
using System.Text;
using BattleBitAPI.Addons.CommandHandler.Common;
using BattleBitAPI.Addons.CommandHandler.Handlers;

namespace BattleBitAPI.Addons.CommandHandler.Modules;

public class HelpModule : CommandModule
{
    private readonly CommandHandlerSettings _commandHandlerSettings;

    public HelpModule(CommandHandlerSettings commandHandlerSettings)
    {
        _commandHandlerSettings = commandHandlerSettings;
    }

    [Command(Name = "help")]
    public Task<bool> HandleHelp()
    {
        var stringBuilder = new StringBuilder();
        foreach (var commandModule in CommandHandlerActivatorService.CommandModules)
            commandModule.Commands.ForEach(command =>
            {
                var formattedParameters = new StringBuilder();

                foreach (var commandParameter in command.Parameters)
                {
                    var attribute = commandParameter.GetCustomAttribute<CommandParameterAttribute>();
                    var parameterName = attribute is null ? commandParameter.Name : attribute.Name;
                    var formattedParameter = commandParameter.HasDefaultValue
                        ? $"{parameterName} Has default value : {commandParameter.DefaultValue}({commandParameter.ParameterType.Name})"
                        : $"{parameterName}({commandParameter.ParameterType.Name})";
                    formattedParameters.Append(formattedParameter).Append(" ");
                }

                stringBuilder
                    .Append(_commandHandlerSettings.CommandRegex)
                    .Append(command.CommandName);


                if (!string.IsNullOrWhiteSpace(formattedParameters.ToString()))
                    stringBuilder
                        .Append(" ")
                        .Append(formattedParameters);

                stringBuilder.Append(" ");

                if (!string.IsNullOrWhiteSpace(command.CommandDescription))
                    stringBuilder.Append("- ")
                        .Append(command.CommandDescription);

                stringBuilder.Append('\n');
            });

        Context.Player.Message(stringBuilder.ToString());
        return Task.FromResult(_commandHandlerSettings.ShowCommandOnChat);
    }
}