namespace MovieInfoSystem.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using MovieInfoSystem.Data;
    using MovieInfoSystem.Data.Models;
    using MovieInfoSystem.Infrastructure;
    using MovieInfoSystem.Models.Authors;
    using System.Linq;

    public class AuthorsController: Controller
    {
        private readonly ApplicationDbContext data;

        public AuthorsController(ApplicationDbContext data)
            => this.data = data;

        [Authorize]
        public IActionResult Create ()
            => View();

        [Authorize]
        [HttpPost]
        public IActionResult Create(BecomeAuthorFormModel author)
        {
            var userIsAlreadyAuthor = this.data
                .Authors
                .Any(x => x.UserId == User.GetId());

            if (userIsAlreadyAuthor)
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
