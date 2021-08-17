namespace MovieInfoSystem.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using MovieInfoSystem.Models.Actors;
    using MovieInfoSystem.Infrastructure;
    using MovieInfoSystem.Services.Authors;
    using Microsoft.AspNetCore.Authorization;
    using MovieInfoSystem.Services.Actors;

    public class ActorsController : Controller
    {
        private readonly IAuthorService authorService;
        private readonly IActorService actor;

        public ActorsController(IAuthorService authorService,
            IActorService actor)
        {
            this.actor = actor;
            this.authorService = authorService;
        }

        public IActionResult All(int currentPage,
            string searchTerm)
            => View(this.actor
                .All(currentPage, searchTerm));

        [Authorize]
        public IActionResult Details(int id)
        {
            var actor = this.actor.Details(id);

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

            if (!this.authorService.IsAuthor(userId))
            {
                return RedirectToAction("Create", "Authors");
            }

            return View();
        }

        [HttpPost]
        [Authorize]
        public IActionResult AddDetails(AddActorDetailsFormModel details, int id)
        {
            if (!this.ModelState.IsValid)
            {
                return View();
            }

            if (this.actor.
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
