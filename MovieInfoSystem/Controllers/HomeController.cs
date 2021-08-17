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
        private readonly IIndexService index;

        public HomeController(ILogger<HomeController> logger, IIndexService index)
        {
            this.index = index;
            _logger = logger;
        }

        public IActionResult Index()
            => View(this.index
                .Home());

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
