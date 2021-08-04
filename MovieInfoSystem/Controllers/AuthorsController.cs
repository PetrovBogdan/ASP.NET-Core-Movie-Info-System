namespace MovieInfoSystem.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using MovieInfoSystem.Data;
    using MovieInfoSystem.Data.Models;
    using MovieInfoSystem.Infrastructure;
    using MovieInfoSystem.Models.Authors;
    using MovieInfoSystem.Services.Authors;
    using System.Linq;

    public class AuthorsController: Controller
    {
        private readonly ApplicationDbContext data;
        private readonly IAuthorService authorService;

        public AuthorsController(ApplicationDbContext data)
            => this.data = data;

        [Authorize]
        public IActionResult Create ()
            => View();

        [Authorize]
        [HttpPost]
        public IActionResult Create(BecomeAuthorFormModel author)
        {

            if (this.authorService.IsAuthor(this.User.GetId()))
            {
                return BadRequest();
            }

            if (!this.ModelState.IsValid)
            {
                return View(author);
            }

            var authorData = new Author
            {
                Name = author.Name,
                Email = author.Email,
                UserId = this.User.GetId(),
            };

            this.data.Authors.Add(authorData);

            this.data.SaveChanges();

            return RedirectToAction("Add", "Movies");
        }
    }
}
