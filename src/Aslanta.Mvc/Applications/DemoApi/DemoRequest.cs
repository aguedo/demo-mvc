using System.ComponentModel.DataAnnotations;

namespace Aslanta.Mvc.Applications.DemoApi;

/// <summary>
/// The demo request.
/// </summary>
public class DemoRequest
{
    /// <summary>
    /// The name of the demo.
    /// </summary>
    [Required]
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
