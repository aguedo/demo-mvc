using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;

namespace Aslanta.Mvc.Controllers.api
{
    [Route("api/demo-cache")]
    [ApiController]
    public class DemoCacheController : ControllerBase
    {
        private static int _cacheCounter = 0;

        [HttpGet("get"), OutputCache(Duration = 10)]
        public IActionResult Get()
        {
            _cacheCounter++;
            return Ok(new
            {
                Message = "Cache accessed successfully!",
                CacheCounter = _cacheCounter
            });
        }

        [HttpGet("get2"), OutputCache(PolicyName = "20Secs")]
        public IActionResult Get2()
        {
            _cacheCounter++;
            return Ok(new
            {
                Message = "Cache accessed successfully!",
                CacheCounter = _cacheCounter
            });
        }

        [HttpGet("bypass-cache")]
        public IActionResult BypassCache()
        {
            _cacheCounter++;
            return Ok(new
            {
                Message = "Cache bypassed successfully!",
                CacheCounter = _cacheCounter
            });
        }
    }
}
