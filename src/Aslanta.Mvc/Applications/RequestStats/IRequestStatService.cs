namespace Aslanta.Mvc.RequestStats;

public interface IRequestStatService
{
    void LogRequest(string path, Request requestStat);
    Dictionary<string, int> GetRequestsCount();
}
