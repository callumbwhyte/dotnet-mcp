using System.Reflection;
using Microsoft.AspNetCore.Authorization;
using Microsoft.SemanticKernel;

namespace MCP_Server.Extensions;

public static class KernelFunctionExtensions
{
    public static string[] GetRoles(this KernelFunction function)
    {
        var roles = function.UnderlyingMethod?.GetCustomAttribute<AuthorizeAttribute>()?.Roles;

        return roles?.Split(',', StringSplitOptions.RemoveEmptyEntries) ?? [];
    }
}