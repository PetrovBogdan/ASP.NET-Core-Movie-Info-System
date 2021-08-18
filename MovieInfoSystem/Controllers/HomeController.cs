namespace MovieInfoSystem.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using MovieInfoSystem.Services.Index;

    public class HomeController : Controller
    {
        private readonly IHomeService home;

        public HomeController(IHomeService home)
        {
            this.home = home;
        }

        public IActionResult Index()
            => View(this.home
                .Index());


        public IActionResult Error()
            => View();

    }
}
