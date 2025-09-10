using System.ComponentModel;
using Microsoft.SemanticKernel;
using MCP_Server;

// Create a kernel builder and add plugins
IKernelBuilder kernelBuilder = Kernel.CreateBuilder();

kernelBuilder.Plugins.AddFromType<MCP_Server.Plugins.VenuePlugin>();

// Build the kernel
Kernel kernel = kernelBuilder.Build();

var builder = Host.CreateEmptyApplicationBuilder(settings: null);

builder.Services.AddMcpServer()
    .WithStdioServerTransport()
    .WithToolsFromAssembly()
    .WithResourcesFromAssembly()
    .WithTools(kernel);

var app = builder.Build();

await app.RunAsync();