using Aslanta.Mvc.Applications.DemoApi;
using Microsoft.AspNetCore.Mvc;

namespace Aslanta.Mvc.Controllers.api
{
    [Route("api/demo")]
    [ApiController]
    public class DemoController : ControllerBase
    {

        /// <summary>
        /// Creates a new demo resource.
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Returns the new demo resource.</returns>
        [HttpPost("create")]
        public ActionResult<DemoResponse> Create(DemoRequest request)
        {
            if (request == null)
            {
                return BadRequest("Request cannot be null.");
            }

            return new DemoResponse
            {
                Id = Guid.NewGuid(),
                Message = "Demo created successfully!",
            };
        }
    }
}
