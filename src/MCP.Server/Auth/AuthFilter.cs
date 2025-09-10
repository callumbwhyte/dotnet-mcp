using Microsoft.AspNetCore.Authentication;
using Microsoft.SemanticKernel;
using MCP_Server.Extensions;

namespace MCP_Server.Auth;

public class AuthInvocationFilter : IAutoFunctionInvocationFilter
{
    private readonly IServiceProvider _serviceProvider;

    public AuthInvocationFilter(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task OnAutoFunctionInvocationAsync(AutoFunctionInvocationContext context, Func<AutoFunctionInvocationContext, Task> next)
    {
        var httpContext = _serviceProvider.GetService<IHttpContextAccessor>()?.HttpContext;

        var user = await httpContext?.AuthenticateAsync();

        var roles = context.Function.GetRoles();

        if (roles?.All(x => user?.Principal?.IsInRole(x) == true) != true)
        {
            context.Result = new FunctionResult(context.Result, "Not authorized to perform this action");

            context.Terminate = true;

            return;
        }

        await next(context);
    }
}