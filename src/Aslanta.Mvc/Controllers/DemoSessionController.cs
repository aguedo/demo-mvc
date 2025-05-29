using Aslanta.Mvc.Applications.DemoSessions;
using Microsoft.AspNetCore.Mvc;

namespace Aslanta.Mvc.Controllers
{
    [Route("demo-session")]
    public class DemoSessionController : Controller
    {
        [HttpGet("")]
        public ActionResult Index()
        {
            ViewBag.Color = HttpContext.Session.GetString("Color");
            ViewBag.Version = HttpContext.Session.GetString("Version");
            ViewBag.Count = HttpContext.Session.GetInt32("Count") ?? 0;
            return View();
        }

        [HttpPost("set-session")]
        public IActionResult SetSession(SessionModel model)
        {
            HttpContext.Session.SetString("Color", model.Color ?? string.Empty);
            HttpContext.Session.SetString("Version", model.Version ?? string.Empty);
            return RedirectToAction("Index");
        }

        [HttpPost("increase-count/{count?}")]
        public IActionResult IncreaseCount(int? count)
        {
            count ??= 1;
            int previousCount = HttpContext.Session.GetInt32("Count") ?? 0;
            HttpContext.Session.SetInt32("Count", previousCount + count.Value);
            return RedirectToAction("Index");
        }
    }
}
