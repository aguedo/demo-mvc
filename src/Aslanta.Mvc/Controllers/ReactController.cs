using Microsoft.AspNetCore.Mvc;

namespace Aslanta.Mvc.Controllers
{
    [Route("react")]
    public class ReactController : Controller
    {
        [HttpGet("index")]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet("dynamic-forms")]
        public ActionResult DynamicForms()
        {
            return View();
        }

        [HttpGet("timeline")]
        public ActionResult Timeline()
        {
            return View();
        }
    }
}
