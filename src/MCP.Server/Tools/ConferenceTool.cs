using System.ComponentModel;
using ModelContextProtocol.Server;

namespace MCP_Server.Tools;

[McpServerToolType]
public static class ConferenceTool
{
    private static Dictionary<string, string> ConferenceDates = new()
    {
        { "NDC Sydney", "2026-04-20" },
        { "NDC Toronto", "2026-05-05" },
        { "NDC Copenhagen", "2026-06-01" },
        { "NDC AI", "2026-06-08" },
        { "NDC Oslo", "2026-09-14" },
        { "NDC TechTown", "2026-09-21" }
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