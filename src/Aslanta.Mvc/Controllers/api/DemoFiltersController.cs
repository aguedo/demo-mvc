using Aslanta.Mvc.Applications.DemoFilters;
using Microsoft.AspNetCore.Mvc;

namespace Aslanta.Mvc.Controllers.api;

[Route("api/demo-filters")]
[ApiController]
public class DemoFiltersController : ControllerBase
{
    [HttpPost("create")]
    [ServiceFilter(typeof(LoggerFilter))]
    public IActionResult Create(DemoFilterRequest request)
    {
        if (request == null)
        {
            return BadRequest("Request cannot be null.");
        }

        return Ok(new { Message = "Demo created successfully!" });
    }

    [HttpPost("update")]
    public IActionResult Update(DemoFilterRequest request)
    {
        if (request == null)
        {
            return BadRequest("Request cannot be null.");
        }

        return Ok(new { Message = "Demo update successfully!" });
    }
}