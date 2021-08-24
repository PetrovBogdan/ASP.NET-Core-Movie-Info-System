namespace MovieInfoSystem.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using MovieInfoSystem.Models.Actors;
    using MovieInfoSystem.Infrastructure;
    using MovieInfoSystem.Services.Actors;
    using MovieInfoSystem.Services.Authors;
    using Microsoft.AspNetCore.Authorization;

    public class ActorsController : Controller
    {
        private readonly IAuthorService authors;
        private readonly IActorService actors;

        public ActorsController(IAuthorService authors,
            IActorService actors)
        {
            this.actors = actors;
            this.authors = authors;
        }

        public IActionResult All(int currentPage,
            string searchTerm)
            => View(this.actors
                .All(currentPage, searchTerm));

        [Authorize]
        public IActionResult Details(int id)
        {
            var actor = this.actors.Details(id);

            if (actor == null)
            {
                return BadRequest();
            }

            return View(actor);
        }

        [Authorize]
        public IActionResult AddDetails()
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
        public IActionResult AddDetails(AddActorDetailsFormModel details, 
            int id)
        {
            if (!this.ModelState.IsValid)
            {
                return View();
            }

            if (this.actors.
                AddDetails(
                details.CountryName,
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
