using System.ComponentModel;
using ModelContextProtocol.Server;

namespace MCP_Server.Resources;

[McpServerResourceType]
public static class ConferenceResource
{
    [McpServerResource]
    [Description("Returns the NDC Sydney endpoint URL.")]
    public static string GetNdcSydneyEndpoint() => "https://ndcsydney.com/";
}