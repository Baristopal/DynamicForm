using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers;
[Route("/")]
public class FormController : Controller
{
    [Authorize(Roles = "User")]
    [HttpGet("/forms")]
    public IActionResult Index()
    {
        return View();
    }

    [HttpGet("/EditForm")]
    public IActionResult EditForm()
    {
        if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            return PartialView("~/Views/Form/EditForm.cshtml");

        return BadRequest("Invalid request");
    }
}
