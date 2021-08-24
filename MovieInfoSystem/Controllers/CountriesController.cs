namespace MovieInfoSystem.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using MovieInfoSystem.Infrastructure;
    using MovieInfoSystem.Services.Authors;
    using MovieInfoSystem.Services.Countries;
    using Microsoft.AspNetCore.Authorization;

    public class CountriesController : Controller
    {
        private readonly ICountriesService countries;
        private readonly IAuthorService authors;

        public CountriesController(ICountriesService countries,
            IAuthorService authors)
        {
            this.authors = authors;
            this.countries = countries;
        }

        public IActionResult All()
            => View(this.countries
                .All());

        [Authorize]
        public IActionResult Details(int id)
        {
            var country = this.countries.Details(id);

            if (country == null)
            {
                return BadRequest();
            }

            return View(country);
        }

        [Authorize]
        public IActionResult AddFlag()
        {
            var userId = this.User.GetId();

            if (!this.authors.IsAuthor(userId))
            {
                return RedirectToAction("Create", "Authors");
            }

            return View();
        }

        [HttpPost]
        [Authorize]
        public IActionResult AddFlag(string flagUrl,
            int id)
        {
            if (!this.ModelState.IsValid)
            {
                return View();
            }

            if (this.countries.AddFlag(flagUrl, id) == false)
            {
                return BadRequest();
            }

            return RedirectToAction(nameof(Details), new { id = id });

        }
    }
}
