namespace Aslanta.Mvc.Applications.DemoFilters;

public class DemoFilterRequest
{
    /// <summary>
    /// The name of the demo.
    /// </summary>
    public string Name { get; set; } = default!;

    /// <summary>
    /// The date of the demo.
    /// </summary>
    public DateTime Date { get; set; }

    /// <summary>
    /// The description explaining the resource.
    /// </summary>
    public string? Description { get; set; }

    public bool? IsActive { get; set; }
}