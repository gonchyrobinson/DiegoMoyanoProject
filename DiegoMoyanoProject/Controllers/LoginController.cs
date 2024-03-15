using Microsoft.AspNetCore.Mvc;

namespace DiegoMoyanoProject.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}