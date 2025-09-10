using Microsoft.SemanticKernel;
using ModelContextProtocol.Server;
using MCP_Server.Auth;

namespace MCP_Server;

public static class KernelExtensions
{
    public static IMcpServerBuilder WithTools(this IMcpServerBuilder builder, Kernel kernel)
    {
        foreach (var plugin in kernel.Plugins)
        {
            foreach (var function in plugin)
            {
                builder.Services.AddSingleton(McpServerTool.Create(function.AsAIFunction()));
            }
        }

        return builder;
    }
}