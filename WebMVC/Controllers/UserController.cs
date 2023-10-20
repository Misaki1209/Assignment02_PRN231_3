using Microsoft.AspNetCore.Mvc;

namespace WebMVC.Controllers;

public class UserController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}