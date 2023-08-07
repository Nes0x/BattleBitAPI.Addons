using System.Text.Json;

namespace BattleBitAPI.Addons.Examples.Services;

public class ConfigService
{
    public ulong AdminId { get; init; }
    
    public static ConfigService Create()
    {
        return JsonSerializer.Deserialize<ConfigService>(File.ReadAllText("appsettings.json"));
    }
}