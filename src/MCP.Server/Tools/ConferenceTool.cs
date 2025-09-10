using System.ComponentModel;
using ModelContextProtocol.Server;

namespace MCP_Server.Tools;

[McpServerToolType]
public static class ConferenceTool
{
    private static Dictionary<string, string> ConferenceDates = new()
    {
        { "Azure Dev Summit", "2025-10-13" },
        { "NDC AI", "2025-11-11" },
        { "NDC Copenhagen", "2025-09-08" },
        { "NDC Manchester", "2025-12-01" },
        { "NDC London", "2026-01-26" },
        { "NDC Oslo", "2026-09-14" },
        { "NDC Porto", "2025-10-20" },
        { "NDC Security", "2026-03-02" },
        { "NDC Sydney", "2026-04-20" }
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