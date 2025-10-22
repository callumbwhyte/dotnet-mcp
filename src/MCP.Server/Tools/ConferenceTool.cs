using System.ComponentModel;
using ModelContextProtocol.Server;

namespace MCP_Server.Tools;

[McpServerToolType]
public static class ConferenceTool
{
    private static Dictionary<string, string> ConferenceDates = new()
    {
        { "Trondheim Developers Conference", "2025-10-20" },
        { "Hello Stavanger", "2025-10-22" },
        { "NIC Rebel", "2025-10-30" },
        { "NDC AI", "2025-11-11" },
        { "NDC Oslo", "2026-09-14" }
    };

    [McpServerTool]
    [Description("Gets the name of the name of a random conference")]
    public static string? RandomConference()
    {
        var random = new Random();

        return ConferenceDates.Keys.Skip(random.Next(ConferenceDates.Keys.Count())).First();
    }

    [McpServerTool]
    [Description("Gets the date of an upcoming conference")]
    public static string? UpcomingConference(string name)
    {
        ConferenceDates.TryGetValue(name, out var date);

        return date;
    }
}