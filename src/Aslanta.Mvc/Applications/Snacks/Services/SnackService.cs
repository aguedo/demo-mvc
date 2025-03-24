using Aslanta.Snacks.Interfaces;
using Newtonsoft.Json;

namespace Aslanta.Snacks.Services;

public class SnackService : ISnackService
{
    private readonly IHttpClientFactory _clientFactory;

    public SnackService(IHttpClientFactory clientFactory)
    {
        _clientFactory = clientFactory;
    }

    public async Task<Snack> GetSnackAsync()
    {
        Task<dynamic?> adviceSlipTask = GetDataAsync("https://api.adviceslip.com/advice");
        Task<dynamic?> chuckJokeTask = GetDataAsync("https://api.chucknorris.io/jokes/random");
        Task<dynamic?> officialJokeTask = GetDataAsync("https://official-joke-api.appspot.com/random_joke");
        Task<dynamic?> uselessFactTask = GetDataAsync("https://uselessfacts.jsph.pl/api/v2/facts/random?amount=1");

        await Task.WhenAll(adviceSlipTask, chuckJokeTask, officialJokeTask, uselessFactTask)
            .ConfigureAwait(false);

        dynamic? adviceSlipData = await adviceSlipTask;
        string adviceSlip = adviceSlipData?.slip?.advice ?? "No advice found";

        dynamic? chuckJokeData = await chuckJokeTask;
        string chuckJoke = chuckJokeData?.value ?? "No joke found";

        dynamic? officialJokeData = await officialJokeTask;
        string? officialJokeSetup = officialJokeData?.setup ?? "No joke found";
        string? officialJokePunchline = officialJokeData?.punchline ?? "No joke found";

        dynamic? uselessFactData = await uselessFactTask;
        string uselessFact = uselessFactData?.text ?? "No fact found";

        return new Snack
        {
            AdviceSlip = adviceSlip,
            ChuckNorrisJoke = chuckJoke,
            UselessFact = uselessFact,
            OfficialJoke = new OfficialJoke
            {
                Setup = officialJokeSetup,
                Punchline = officialJokePunchline
            }
        };
    }

    private async Task<dynamic?> GetDataAsync(string url)
    {
        try
        {
            string json = await GetStringAsync(url);
            return JsonConvert.DeserializeObject(json);
        }
        catch (Exception)
        {
            return null;
        }
    }

    private async Task<T?> GetDataAsync<T>(string url)
    {
        try
        {
            string json = await GetStringAsync(url);
            return JsonConvert.DeserializeObject<T>(json);
        }
        catch (Exception)
        {
            return default;
        }
    }

    private async Task<string> GetStringAsync(string url)
    {
        var client = _clientFactory.CreateClient();
        HttpResponseMessage response = await client.GetAsync(url);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync();
    }
}
