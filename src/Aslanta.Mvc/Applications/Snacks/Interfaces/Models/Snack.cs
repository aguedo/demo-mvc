namespace Aslanta.Snacks.Interfaces;

public record Snack
{
    public required string AdviceSlip { get; init; }
    public required string ChuckNorrisJoke { get; init; }
    public required string UselessFact { get; init; }
    public required OfficialJoke OfficialJoke { get; init; }
}
