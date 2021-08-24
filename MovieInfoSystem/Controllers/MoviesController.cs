namespace MovieInfoSystem.Controllers
{
    using System.Linq;

    using Microsoft.AspNetCore.Mvc;
    using MovieInfoSystem.Data.Models;
    using MovieInfoSystem.Models.Movies;
    using MovieInfoSystem.Infrastructure;
    using MovieInfoSystem.Services.Movies;
    using MovieInfoSystem.Services.Authors;
    using Microsoft.AspNetCore.Authorization;
    using MovieInfoSystem.Services.Movies.Models;

    using static WebConstants;

    public class MoviesController : Controller
    {
        private readonly IAuthorService authors;
        private readonly IMovieService movies;

        public MoviesController(IAuthorService authors,
            IMovieService movies)
        {
            this.authors = authors;
            this.movies = movies;
        }

        [Authorize]
        public IActionResult Add()
        {
            var userId = this.GetUserId();

            if (!authors.IsAuthor(userId) && !this.User.IsInRole(AdministratorRoleName))
            {
                return RedirectToAction("Create", "Authors");
            }

            return View(new MovieServiceModel
            {
                Genres = this.movies.GetGenres(),
            });
        }

        [HttpPost]
        [Authorize]
        public IActionResult Add(MovieFormModel movie)
        {
            var userId = this.GetUserId();
            var authorId = this.authors.GetId(userId);
            var userIsAdmin = this.User.IsInRole(AdministratorRoleName);
            this.ValidateModelState(movie);

            if (!ModelState.IsValid)
            {
                movie.Genres = this.movies.GetGenres();
                var movieAsServiceModel = new MovieServiceModel
                {
                    Id = movie.Id,
                    Title = movie.Title,
                    Summary = movie.Summary,
                    Audio = movie.Audio,
                    Duration = movie.Duration,
                    ImageUrl = movie.ImageUrl,
                    Actors = movie.Actors,
                    Directors = movie.Directors,
                    Countries = movie.Countries,
                    GenreId = movie.GenreId,
                    Genres = movie.Genres,
                };
                return View(movieAsServiceModel);
            }

            if (authorId == 0 && !userIsAdmin)
            {
                return RedirectToAction("Create", "Authors");
            }

            this.movies.Create(movie.Title,
                     movie.Summary,
                     movie.Duration,
                     movie.ImageUrl,
                     movie.Audio,
                     authorId,
                     userIsAdmin,
                     userId,
                     movie.GenreId,
                     movie.Actors,
                     movie.Directors,
                     movie.Countries);

            return RedirectToAction("All", "Movies");
        }

        public IActionResult All(int currentPage,
            string searchTerm)
            => View(this.movies
                .All(searchTerm, currentPage));

        [Authorize]
        public IActionResult Details(int id)
        {

            var userId = this.GetUserId();

            var movie = this.movies.Details(id, userId);

            if (movie == null)
            {
                return BadRequest();
            }

            return View(movie);
        }

        [Authorize]
        public IActionResult Mine(string userId)
            => View(this.movies
                .Mine(userId));

        [Authorize]
        public IActionResult Edit(int id)
        {
            var movieCreator = this.movies.GetCreatorId(id);
            var userId = this.User.GetId();
            var userIsInRole = this.User.IsInRole(AdministratorRoleName);

            if (movieCreator != userId && !userIsInRole)
            {
                return Unauthorized();
            }

            var movie = this.movies.GetEditDetails(id);

            return View(movie);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Edit(int id, 
            MovieFormModel movie)
        {

            if (!ModelState.IsValid)
            {
                return View(this.movies.GetEditDetails(id));
            }

            var creatorId = this.movies.GetCreatorId(id);
            var userId = this.User.GetId();
            var userIsInRole = this.User.IsInRole(AdministratorRoleName);

            if (creatorId != userId && !userIsInRole)
            {
                return Unauthorized();
            }

            this.movies.Edit(id,
                 movie.Title,
                 movie.Summary,
                 movie.Duration,
                 movie.ImageUrl,
                 movie.Audio,
                 movie.Actors,
                 movie.Directors,
                 movie.Countries);

            return RedirectToAction(nameof(Details), new { id = id });
        }

        [HttpPost]
        [Authorize]
        public IActionResult AddComment(int id, 
            string comment)
        {
            var userId = this.GetUserId();

            if (string.IsNullOrWhiteSpace(comment))
            {
                this.ModelState.AddModelError(nameof(Comment), "The comment must be at least 5 characters long.");
            }

            var currComment = this.movies.AddComment(id, comment, userId);

            if (!this.ModelState.IsValid)
            {
                return RedirectToAction(nameof(Details), new { id = id });
            }

            if (currComment == false)
            {
                return RedirectToAction("Create", "Authors");
            }

            return RedirectToAction(nameof(Details), new { id = id });
        }

        [Authorize]
        public IActionResult Delete(int id)
        {
            if (!this.movies.Delete(id))
            {
                return BadRequest();
            }

            return RedirectToAction("All", "Movies");
        }

        [Authorize]
        public IActionResult RemoveDirector(int directorId,
            string title)
        {
            var movieId = this.movies.RemovieDirector(directorId, title);

            return RedirectToAction(nameof(Edit), new { Id = movieId });
        }

        [Authorize]
        public IActionResult RemoveActor(int actorId, 
            string title)
        {
            var movieId = this.movies.RemoveActor(actorId, title);

            return RedirectToAction(nameof(Edit), new { Id = movieId });

        }

        [Authorize]
        public IActionResult RemoveCountry(int countryId,
            string title)
        {
            var movieId = this.movies.RemoveCountry(countryId, title);

            return RedirectToAction(nameof(Edit), new { Id = movieId });
        }

        private void ValidateModelState(MovieFormModel movie)
        {
            if (movie.GenreId != null)
            {
                foreach (var genreId in movie.GenreId)
                {
                    if (!this.movies.GetGenres().Any(x => x.Id == genreId))
                    {
                        this.ModelState.AddModelError(nameof(movie.GenreId),
                            "The selected genre does not exist!");
                    }
                }
            }
            else
            {
                this.ModelState.AddModelError(nameof(movie.Genres),
                    "You must enter at least 1 genre in order to create a movie.");
            }

            if (movie.Actors == null)
            {
                this.ModelState.AddModelError(nameof(movie.Actors),
                    "You must enter at least 1 actor in order to create a movie.");
            }
            if (movie.Directors == null)
            {
                this.ModelState.AddModelError(nameof(movie.Directors),
                    "You must enter at least 1 director in order to create a movie.");
            }
            if (movie.Countries == null)
            {
                this.ModelState.AddModelError(nameof(movie.Countries),
                    "You must enter at least 1 country in order to create a movie.");
            }

        }

        private string GetUserId()
            => this.User
            .GetId();

    }
}
