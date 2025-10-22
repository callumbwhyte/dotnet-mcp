using System.ComponentModel;
using ModelContextProtocol.Server;

namespace MCP_Server.Resources;

[McpServerResourceType]
public static class ConferenceResource
{
    [McpServerResource]
    [Description("Returns the Hello Stavanger endpoint URL.")]
    public static string GetHelloStavangerEndpoint() => "https://www.hellostavanger.no/talks/";
}