using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Aslanta.Mvc.Controllers;

[Route("demo-auth0")]
public class DemoAuth0Controller : Controller
{
    [HttpGet("")]
    [Authorize]
    public ActionResult Index()
    {
        ViewBag.User = new
        {
            Name = User.Claims.FirstOrDefault(c => c.Type == "name")?.Value,
            EmailAddress = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value,
            ProfileImage = User.Claims.FirstOrDefault(c => c.Type == "picture")?.Value
        };

        return View();
    }
}