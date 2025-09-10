var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.MCP_Server>("mcp-server");

builder.Build().Run();