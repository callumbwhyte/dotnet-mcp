using System.ComponentModel;
using ModelContextProtocol.Server;

namespace MCP_Server.Resources;

[McpServerResourceType]
public static class ConferenceResource
{
    [McpServerResource]
    [Description("Returns the NDC Copenhagen endpoint URL.")]
    public static string GetNdcCopenhagenEndpoint() => "https://ndccopenhagen.com/";
}