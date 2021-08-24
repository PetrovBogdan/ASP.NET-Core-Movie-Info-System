namespace MovieInfoSystem.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using MovieInfoSystem.Infrastructure;
    using MovieInfoSystem.Services.Authors;
    using MovieInfoSystem.Models.Directors;
    using Microsoft.AspNetCore.Authorization;
    using MovieInfoSystem.Services.Directors;

    public class DirectorsController : Controller
    {
        private readonly IAuthorService author;
        private readonly IDirectorService director;

        public DirectorsController(IAuthorService author,
            IDirectorService director)
        {
            this.director = director;
            this.author = author;
        }

        public IActionResult All(int currentPage,
            string searchTerm)
            => View(this.director
                .All(currentPage, searchTerm));

        [Authorize]
        public IActionResult Details(int id)
        {
            var director = this.director.Details(id);

            if (director == null)
            {
                return BadRequest();
            }

            return View(director);
        }

        [Authorize]
        public IActionResult AddDetails()
        {
            var userId = this.User.GetId();

            if (!this.author.IsAuthor(userId))
            {
                return RedirectToAction("Create", "Authors");
            }

            return View();
        }

        [HttpPost]
        [Authorize]
        public IActionResult AddDetails(AddDirectorDetailsFormModel details, int id)
        {
            if (!this.ModelState.IsValid)
            {
                return View();
            }

            if (this.director
                .AddDetails
                (details.CountryName,
                details.Biography,
                details.Picture,
                id) == false)
            {
                return BadRequest();
            }

            return RedirectToAction(nameof(Details), new { id = id });
        }
    }
}
