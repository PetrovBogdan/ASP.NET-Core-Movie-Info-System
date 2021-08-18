namespace MovieInfoSystem.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using MovieInfoSystem.Models.Authors;
    using MovieInfoSystem.Infrastructure;
    using MovieInfoSystem.Services.Authors;
    using Microsoft.AspNetCore.Authorization;

    using static WebConstants;

    public class AuthorsController : Controller
    {
        private readonly IAuthorService author;

        public AuthorsController(IAuthorService author) 
            => this.author = author;

        [Authorize]
        public IActionResult Create()
            => View();

        [Authorize]
        [HttpPost]
        public IActionResult Create(BecomeAuthorFormModel author)
        {
            var userId = this.User.GetId();

            if (this.author.IsAuthor(userId) || this.User.IsInRole(AdministratorRoleName))
            {
                return BadRequest();
            }

            if (!this.ModelState.IsValid)
            {
                return View(author);
            }

            this.author.Create(author.Name, author.Email, userId);

            return RedirectToAction("Add", "Movies");
        }
    }
}
