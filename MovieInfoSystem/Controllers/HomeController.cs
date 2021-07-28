namespace MovieInfoSystem.Controllers
{
    using System.Linq;
    using System.Diagnostics;

    using MovieInfoSystem.Data;
    using MovieInfoSystem.Models;
    using Microsoft.AspNetCore.Mvc;
    using MovieInfoSystem.Models.Index;
    using Microsoft.Extensions.Logging;

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext data;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext data)
        {
            this.data = data;
            _logger = logger;
        }

        public IActionResult Index()
        {
            var totalMovies = this.data.Movies.Count();
            var totalActors = this.data.Actors.Count();
            var totalDirectors = this.data.Directors.Count();
            var totalUsers = this.data.Users.Count();

            var movies = this.data
                .Movies
                .OrderByDescending(x => x.Id)
                .Select(x => new MovieIndexViewModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    Image = x.Image,
                })
                .Take(3)
                .ToList();

            return View(new IndexViewModel
            {
                TotalMovies = totalMovies,
                TotalActors = totalActors,
                TotalDirectors = totalDirectors,
                TotalUsers = totalUsers,
                Movies = movies,
            });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
