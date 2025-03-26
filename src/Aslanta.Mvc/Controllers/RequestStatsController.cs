using Aslanta.Mvc.RequestStats;
using Microsoft.AspNetCore.Mvc;

namespace Aslanta.Mvc.Controllers
{
    [Route("request-stats")]
    public class RequestStatsController : Controller
    {
        private IRequestStatService _requestStatService;
        public RequestStatsController(IRequestStatService requestStatService)
        {
            _requestStatService = requestStatService;
        }

        [HttpGet("")]
        public ActionResult Index()
        {
            ViewBag.RequestStats = _requestStatService.GetRequestsCount()
                .OrderByDescending(x => x.Value);
            return View();
        }

    }
}
