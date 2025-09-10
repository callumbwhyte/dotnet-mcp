using System.Text.Json;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.AI;
using Microsoft.SemanticKernel;
using MCP_Server.Extensions;

namespace MCP_Server.Auth;

public class AuthorizedAIFunction : AIFunction
{
    private readonly Kernel _kernel;
    private readonly KernelFunction _function;
    private readonly AIFunction _aiFunction;

    public AuthorizedAIFunction(Kernel kernel, KernelFunction function)
    {
        _kernel = kernel;
        _function = function;
        _aiFunction = function.AsAIFunction();
    }

    public override string Name => _aiFunction.Name;

    public override string Description => _aiFunction.Description;

    public override JsonElement JsonSchema => _aiFunction.JsonSchema;

    public override JsonSerializerOptions JsonSerializerOptions => _aiFunction.JsonSerializerOptions;

    protected override async ValueTask<object?> InvokeCoreAsync(AIFunctionArguments arguments, CancellationToken cancellationToken)
    {
        var httpContext = _kernel.Services.GetService<IHttpContextAccessor>()?.HttpContext;

        var user = await httpContext?.AuthenticateAsync();

        var roles = _function.GetRoles();

        if (roles?.All(x => user?.Principal?.IsInRole(x) == true) != true)
        {
            throw new Exception("Not authorized to perform this action");
        }

        return await _function.InvokeAsync(_kernel);
    }
}