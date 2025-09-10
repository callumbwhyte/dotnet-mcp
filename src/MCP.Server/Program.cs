using System.ComponentModel;
using Microsoft.SemanticKernel;
using MCP_Server;

// Create a kernel builder and add plugins
IKernelBuilder kernelBuilder = Kernel.CreateBuilder();

kernelBuilder.Plugins.AddFromType<MCP_Server.Plugins.VenuePlugin>();

// Build the kernel
Kernel kernel = kernelBuilder.Build();

var builder = WebApplication.CreateBuilder(args);

// Add service defaults & Aspire client integrations.
builder.AddServiceDefaults();

builder.Services.AddMcpServer()
    .WithHttpTransport()
    //.WithStdioServerTransport()
    .WithToolsFromAssembly()
    .WithResourcesFromAssembly()
    .WithTools(kernel);

var app = builder.Build();

app.MapMcp();

app.Run("http://localhost:3001/");

await app.RunAsync();