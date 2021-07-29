namespace MovieInfoSystem.Controllers
{
    using System.Linq;
    using System.Collections.Generic;

    using MovieInfoSystem.Data;
    using Microsoft.AspNetCore.Mvc;
    using MovieInfoSystem.Data.Models;
    using MovieInfoSystem.Models.Movies;
    using MovieInfoSystem.Infrastructure;
    using Microsoft.AspNetCore.Authorization;

    public class MoviesController : Controller
    {
        private readonly ApplicationDbContext data;

        public MoviesController(ApplicationDbContext data)
            => this.data = data;

        [Authorize]
        public IActionResult Add()
        {

            if (!this.UserIsAuthor)
            {
                return RedirectToAction("Create", "Authors");
            }

            return View(new AddMovieFormModel
            {
                Genres = this.GetMovieGenres(),
            });
        }


        [HttpPost]
        [Authorize]
        public IActionResult Add(AddMovieFormModel movie)
        {
            var authorId = this.data
                .Authors
                .Where(x => x.UserId == this.User.GetId())
                .Select(x => x.Id)
                .FirstOrDefault();

            if (movie.GenreId != null)
            {
                foreach (var genreId in movie.GenreId)
                {
                    if (!this.data.Genres.Any(x => x.Id == genreId))
                    {
                        this.ModelState.AddModelError(nameof(movie.GenreId), "The selected genre does not exist!");
                    }
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
                AuthorId = authorId,
            };

            if (authorId == 0)
            {
                return RedirectToAction("Create", "Authors");
            }

            foreach (var genreId in movie.GenreId)
            {
                var genre = this.data.Genres.First(x => x.Id == genreId);

                movieData.Genres.Add(new GenreMovie
                {
                    GenreId = genreId.Value,
                    Genre = genre,
                });
            }

            foreach (var actor in movie.Actors.Where(x => x.FirstName != null || x.LastName != null))
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


            foreach (var director in movie.Directors.Where(x => x.FirstName != null || x.LastName != null))
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

                movieData.Directors.Add(new DirectorMovie { Director = currDirector });
            }

            foreach (var country in movie.Countries.Where(x => x.Name != null))
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

            return RedirectToAction("All", "Movies");
        }

        public IActionResult All(int currentPage, string searchTerm)
        {
            var moviesQuery = this.data.Movies.AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                moviesQuery = moviesQuery
                    .Where(m => m.Title.ToLower().Contains(searchTerm.ToLower()));
            }

            if (currentPage == 0)
            {
                currentPage = 1;
            }

            var totalMovies = this.data.Movies.Count();

            var movies = moviesQuery
                .OrderByDescending(x => x.Id)
                .Skip((currentPage - 1) * AllMoviesViewModel.MoviesPerPage)
                .Take(AllMoviesViewModel.MoviesPerPage)
                .Select(x => new MovieListingViewModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    Audio = x.Audio,
                    Image = x.Image,
                    Summary = x.Summary,
                    Actors = x.Actors.Select(a => new MovieActorsViewModel
                    {
                        Id = a.Actor.Id,
                        FirstName = a.Actor.FirstName,
                        LastName = a.Actor.LastName,

                    }).ToList(),
                    Directors = x.Directors.Select(d => new MovieDirectorsViewModel
                    {
                        Id = d.Director.Id,
                        FirstName = d.Director.FirstName,
                        LastName = d.Director.LastName,
                    }).ToList(),
                    Countries = x.Countries.Select(c => new MovieCountriesViewMOdel
                    {
                        Id = c.Country.Id,
                        Name = c.Country.Name
                    }).ToList()

                })
                .ToList();

            return View(new AllMoviesViewModel
            {
                TotalMovies = totalMovies,
                CurrentPage = currentPage,
                Movies = movies,
            });
        }

        [Authorize]
        public IActionResult Details(int id)
        {

            var movie = this.data
                .Movies
                .Where(x => x.Id == id)
                .Select(x => new MovieDetailsViewModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    Audio = x.Audio,
                    Image = x.Image,
                    Summary = x.Summary,
                    AuthorId = x.AuthorId,
                    CreatedOn = x.CreatedOn.ToString("dddd, dd MMMM yyyy"),
                    Duration = x.Duration,
                    Genres = x.Genres.Select(g => new MovieGenreViewModel
                    {
                        Id = g.Genre.Id,
                        Type = g.Genre.Type,

                    }).ToList(),
                    Actors = x.Actors.Select(a => new MovieActorsViewModel
                    {
                        Id = a.Actor.Id,
                        FirstName = a.Actor.FirstName,
                        LastName = a.Actor.LastName,
                    }).ToList(),
                    Directors = x.Directors.Select(d => new MovieDirectorsViewModel
                    {
                        Id = d.Director.Id,
                        FirstName = d.Director.FirstName,
                        LastName = d.Director.LastName,
                    }).ToList(),
                    Countries = x.Countries.Select(c => new MovieCountriesViewMOdel
                    {
                        Id = c.Country.Id,
                        Name = c.Country.Name,
                    }).ToList()
                })
                .FirstOrDefault();

            if (movie == null)
            {
                return BadRequest();
            }

            return View(movie);
        }


        private bool UserIsAuthor
            => this.data
                .Authors
                .Any(x => x.UserId == this.User.GetId());

        private ICollection<MovieGenreViewModel> GetMovieGenres()
            => data
            .Genres
            .Select(x => new MovieGenreViewModel
            {
                Id = x.Id,
                Type = x.Type,
            })
            .OrderBy(x => x.Type)
            .ToList();
    }
}
