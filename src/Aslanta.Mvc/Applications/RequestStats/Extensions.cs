namespace Aslanta.Mvc.RequestStats;

public static class Extensions
{
    public static IApplicationBuilder UseRequestStatsMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<RequestStatsMiddleware>();
    }
}
