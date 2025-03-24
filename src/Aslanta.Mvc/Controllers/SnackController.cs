using Aslanta.Snacks.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Aslanta.Mvc.Controllers
{
    [Route("snack")]
    public class SnackController : Controller
    {
        private readonly ISnackService _snackService;

        public SnackController(ISnackService snackService)
        {
            _snackService = snackService;
        }

        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            Snack snacks = await _snackService.GetSnackAsync();

            ViewBag.AdviceSlip = snacks.AdviceSlip;
            ViewBag.ChuckNorrisJoke = snacks.ChuckNorrisJoke;
            ViewBag.UselessFact = snacks.UselessFact;
            ViewBag.OfficialJoke = new
            {
                snacks.OfficialJoke.Setup,
                snacks.OfficialJoke.Punchline
            };

            return View(snacks);
        }

    }
}
