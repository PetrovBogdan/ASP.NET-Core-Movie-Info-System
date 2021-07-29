namespace MovieInfoSystem.Controllers
{
    using System.Linq;

    using MovieInfoSystem.Data;
    using Microsoft.AspNetCore.Mvc;
    using MovieInfoSystem.Models.Actors;
    using Microsoft.AspNetCore.Authorization;

    public class ActorsController : Controller
    {
        private readonly ApplicationDbContext data;

        public ActorsController(ApplicationDbContext data) 
            => this.data = data;

        [Authorize]
        public IActionResult All()
        {
            var actors = this.data
                .Actors
                .Select(x => new ActorsListingViewModel
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Country = x.Country.Name,
                    Biography = x.Biography,
                    Picture = x.Picture,
                    Movies = x.Movies
                                .Select(x => x.Movie.Title)
                                .ToList()
                }).ToList();

            if (actors == null)
            {
                return BadRequest();
            }

            return View(actors);
        }
    }
}
