using DiegoMoyanoProject.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using DiegoMoyanoProject.ViewModels;
using DiegoMoyanoProject.ViewModels.Home;

namespace DiegoMoyanoProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View(new IndexHomeViewModel(IsLogued()));
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        private bool IsLogued()
        {
            return !(!HttpContext.Session.IsAvailable || HttpContext.Session.GetString("Mail") == null);
        }
    }



}
