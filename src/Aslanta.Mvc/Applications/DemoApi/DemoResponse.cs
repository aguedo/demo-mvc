namespace Aslanta.Mvc.Applications.DemoApi;

/// <summary>
/// The demo response.
/// </summary>
public class DemoResponse
{
    /// <summary>
    /// The unique identifier of the response.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// The message of the response.
    /// </summary>
    public string Message { get; set; } = default!;
}
