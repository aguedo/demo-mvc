using System.Collections.Concurrent;

namespace Aslanta.Mvc.RequestStats;

public class RequestStatService : IRequestStatService
{
    private readonly ConcurrentDictionary<string, ConcurrentBag<Request>> _requestStats = new();

    public void LogRequest(string path, Request requestStat)
    {
        _requestStats.AddOrUpdate(path,
            (_) => new ConcurrentBag<Request>([requestStat]),
            (key, concurrentBag) =>
            {
                concurrentBag.Add(requestStat);
                return concurrentBag;
            });
    }

    public Dictionary<string, int> GetRequestsCount()
    {
        return _requestStats.ToDictionary(x => x.Key, x => x.Value.Count);
    }
}
