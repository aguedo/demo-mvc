using System.Diagnostics;

namespace Aslanta.Mvc.RequestStats;

public class RequestStatsMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IRequestStatService _statService;

    public RequestStatsMiddleware(RequestDelegate next, IRequestStatService statService)
    {
        _next = next;
        _statService = statService;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var request = new Request { DateTime = DateTime.Now };
        _statService.LogRequest(context.Request.Path, request);

        var watch = new Stopwatch();
        watch.Start();

        await _next(context);

        watch.Stop();
        request.TimeInMiliseconds = watch.ElapsedMilliseconds;
    }
}
