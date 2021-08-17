namespace MovieInfoSystem.Controllers
{
    using System.Diagnostics;

    using MovieInfoSystem.Models;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using MovieInfoSystem.Services.Index;

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHomeService home;

        public HomeController(ILogger<HomeController> logger, IHomeService home)
        {
            this.home = home;
            _logger = logger;
        }

        public IActionResult Index()
            => View(this.home
                .Index());

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
