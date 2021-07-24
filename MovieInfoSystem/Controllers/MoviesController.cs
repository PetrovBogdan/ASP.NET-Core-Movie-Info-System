namespace MovieInfoSystem.Controllers
{
    using System.Linq;
    using System.Collections.Generic;

    using MovieInfoSystem.Data;
    using Microsoft.AspNetCore.Mvc;
    using MovieInfoSystem.Models.Movies;
    using MovieInfoSystem.Data.Models;

    public class MoviesController : Controller
    {
        private readonly ApplicationDbContext data;

        public MoviesController(ApplicationDbContext data)
            => this.data = data;

        public IActionResult Add()
            => View(new AddMovieFormModel
            {
                Genres = this.GetMovieGenres(),
            });

        [HttpPost]
        public IActionResult Add(AddMovieFormModel movie)
        {
            foreach (var genreId in movie.GenreId)
            {
                if (!this.data.Genres.Any(x => x.Id == genreId))
                {
                    this.ModelState.AddModelError(nameof(movie.GenreId), "The selected genre does not exist!");
                }
            }

            if (!ModelState.IsValid)
            {
                movie.Genres = this.GetMovieGenres();
                return View(movie);
            }

            var movieData = new Movie
            {
                Title = movie.Title,
                Summary = movie.Summary,
                Duration = movie.Duration,
                Image = movie.ImageUrl,
                Audio = movie.Audio,
            };

            foreach (var genreId in movie.GenreId)
            {
                var genre = this.data.Genres.FirstOrDefault(x => x.Id == genreId);

                movieData.Genres.Add(new GenreMovie
                {
                    GenreId = genreId,
                    Genre = genre,
                });
            }

            foreach (var actor in movie.Actors)
            {
                var currActor = this.data
                    .Actors
                    .FirstOrDefault(x => x.FirstName == actor.FirstName && x.LastName == actor.LastName);

                if (currActor == null)
                {
                    currActor = new Actor
                    {
                        FirstName = actor.FirstName,
                        LastName = actor.LastName,
                    };
                }

                movieData.Actors.Add(new ActorMovie { Actor = currActor });
            }


            foreach (var director in movie.Directors)
            {
                var currDirector = this.data
                    .Directors
                    .FirstOrDefault(x => x.FirstName == director.FirstName && x.LastName == director.LastName);

                if (currDirector == null)
                {
                    currDirector = new Director
                    {
                        FirstName = director.FirstName,
                        LastName = director.LastName,
                    };
                }

                movieData.Directors.Add(new DirectorMovie { Director = currDirector});
            }

            foreach (var country in movie.Countries)
            {
                var currCountry = this.data
                    .Countries
                    .FirstOrDefault(x => x.Name == country.Name);

                if (currCountry == null)
                {
                    currCountry = new Country
                    {
                        Name = country.Name,
                    };
                }

                movieData.Countries.Add(new CountryMovie { Country = currCountry });
            }

            this.data.Movies.Add(movieData);

            this.data.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

        private ICollection<MovieGenreViewModel> GetMovieGenres()
            => data
            .Genres
            .Select(x => new MovieGenreViewModel
            {
                Id = x.Id,
                Type = x.Type,
            })
            .ToList();
    }
}
