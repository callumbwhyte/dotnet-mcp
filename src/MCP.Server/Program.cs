using Microsoft.SemanticKernel;
using ModelContextProtocol.AspNetCore.Authentication;
using MCP_Server;
using MCP_Server.Auth;

// Create a kernel builder and add plugins
IKernelBuilder kernelBuilder = Kernel.CreateBuilder();

kernelBuilder.Plugins.AddFromType<MCP_Server.Plugins.VenuePlugin>();

kernelBuilder.Services.AddSingleton<IAutoFunctionInvocationFilter, AuthInvocationFilter>();

// Build the kernel
Kernel kernel = kernelBuilder.Build();

var builder = WebApplication.CreateBuilder(args);

// Add service defaults & Aspire client integrations.
builder.AddServiceDefaults();

builder.Services.AddAuthentication(options =>
{
    options.DefaultChallengeScheme = McpAuthenticationDefaults.AuthenticationScheme;
    options.DefaultAuthenticateScheme = McpAuthenticationDefaults.AuthenticationScheme;
})
.AddMcp(options =>
{
    options.ResourceMetadata = new()
    {
        Resource = new Uri("https://localhost:7279/"),
        AuthorizationServers = { new Uri("https://localhost:7279/") },
        ScopesSupported = ["mcp:tools"],
    };
});

builder.Services.AddAuthorization();

builder.Services.AddMcpServer()
    .WithHttpTransport()
    //.WithStdioServerTransport()
    .WithToolsFromAssembly()
    .WithResourcesFromAssembly()
    .WithTools(kernel);

var app = builder.Build();

app.UseAuthentication();

app.UseAuthorization();

app.MapMcp().RequireAuthorization();

app.Run("http://localhost:3001/");

await app.RunAsync();