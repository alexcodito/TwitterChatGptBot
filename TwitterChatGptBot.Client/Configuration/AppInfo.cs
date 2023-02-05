using System.Reflection;

namespace TwitterChatGptBot.Client.Configuration;

internal static class AppInfo
{
    public static string? Version => Assembly.GetExecutingAssembly().GetName().Version?.ToString();

    public static string ConfigurationPath =>
        Path.Combine(
            Environment.GetEnvironmentVariable("AppData") ?? "",
            Assembly.GetExecutingAssembly().GetName().Name ?? "",
            "configuration.json");
}