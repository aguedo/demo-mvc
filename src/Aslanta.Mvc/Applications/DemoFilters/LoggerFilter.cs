using Microsoft.AspNetCore.Mvc.Filters;

namespace Aslanta.Mvc.Applications.DemoFilters;

public class LoggerFilter : IAsyncActionFilter
{
    private readonly ILogger<LoggerFilter> _logger;

    public LoggerFilter(ILogger<LoggerFilter> logger)
    {
        _logger = logger;
    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        string traceIdentifier = context.HttpContext.TraceIdentifier;
        object controller = context.Controller;
        string? actionName = context.ActionDescriptor.DisplayName;
        string payload = string.Join(", ", context.ActionArguments
            .Select(arg => $"{arg.Key}: {arg.Value}"));
        _logger.LogInformation("Action Log Request ({TraceIdentifier}): Controller={Controller}, Action={Action}, Payload={Payload}",
                traceIdentifier, controller, actionName, payload);

        ActionExecutedContext? executedCtx = await next();

        if (executedCtx.Exception != null)
        {
            _logger.LogError(executedCtx.Exception, "Action Log Error ({TraceIdentifier}): Controller={Controller}, Action={Action}",
                traceIdentifier, controller, actionName);
        }
        else
        {
            _logger.LogInformation("Action Log Response ({TraceIdentifier}): Controller={Controller}, Action={Action}, Response={Response}",
                traceIdentifier, context.Controller, context.ActionDescriptor.DisplayName, executedCtx.Result);
        }
    }
}


