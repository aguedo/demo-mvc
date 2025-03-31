using Microsoft.AspNetCore.Mvc;

namespace Aslanta.Mvc.Controllers
{
    [Route("react")]
    public class ReactController : Controller
    {
        // GET: ReactController
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet("dynamic-forms")]
        public ActionResult DynamicForms()
        {
            var data = new { Name = "Aslanta", Version = "1.0" };
            return View();
        }
    }
}
