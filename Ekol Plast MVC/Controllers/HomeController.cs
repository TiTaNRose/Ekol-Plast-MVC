using Ekol_Plast_MVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Ekol_Plast_MVC.Controllers
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
            return View();
        }
        public IActionResult Gallery()
        {
            return View();
        }
        public IActionResult Profiles()
        {
            return View();
        }

        public IActionResult AboutUs()
        {
            return View();
        }

        public IActionResult Katalog()
        {
            return Redirect("/katalog.pdf");
        }
    }
}