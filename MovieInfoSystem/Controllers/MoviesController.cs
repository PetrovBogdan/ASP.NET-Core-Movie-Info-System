namespace MovieInfoSystem.Controllers
{
    using System.Linq;
    using System.Collections.Generic;

    using MovieInfoSystem.Data;
    using Microsoft.AspNetCore.Mvc;
    using MovieInfoSystem.Data.Models;
    using MovieInfoSystem.Models.Movies;
    using MovieInfoSystem.Infrastructure;
    using MovieInfoSystem.Services.Authors;
    using Microsoft.AspNetCore.Authorization;

    public class MoviesController : Controller
    {
        private readonly ApplicationDbContext data;
        private readonly IAuthorService authorService;

        public MoviesController(ApplicationDbContext data, IAuthorService authorService)
        {
            this.authorService = authorService;
            this.data = data;
        }

        [Authorize]
        public IActionResult Add()
        {
            var userId = this.User.GetId();

            if (!authorService.IsAuthor(userId))
            {
                return RedirectToAction("Create", "Authors");
            }

            return View(new MovieFormModel
            {
                Genres = this.GetMovieGenres(),
            });
        }

        [HttpPost]
        [Authorize]
        public IActionResult Add(MovieFormModel movie)
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
                        this.ModelState.AddModelError(nameof(movie.GenreId),
                            "The selected genre does not exist!");
                    }
                }
            }
            else
            {
                this.ModelState.AddModelError(nameof(movie.Actors),
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
                Creator = this.User.GetId(),
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

            this.AddActors(movie, movieData);
            this.AddDirectors(movie, movieData);
            this.AddCountries(movie, movieData);

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
                SearchTerm = searchTerm,
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
                    IsCreator = x.Creator == this.User.GetId(),
                    Comments = x.Comments.Select(c => new MovieCommentsViewModel
                    {
                        Id = c.Id,
                        AuthorName = c.Author.Name,
                        Body = c.Body,
                        CreatedOn = c.CreatedOn.ToString("dddd, dd MMMM yyyy")
                    }).ToList(),
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

        [Authorize]
        public IActionResult Mine(string userId)
        {
            var movies = this.data
                .Movies
                .Where(x => x.Creator == userId)
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


            return View(movies);
        }


        [Authorize]
        public IActionResult Edit(int id)
        {
            var movieCreator = this.GetMovieCreatorId(id);

            if (movieCreator != this.User.GetId())
            {
                return Unauthorized();
            }

            var movie = this.GetEditMovieDetails(id);

            return View(movie);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Edit(int id, MovieFormModel movie)
        {

            if (!ModelState.IsValid)
            {
                return View(this.GetEditMovieDetails(id));
            }

            if (this.GetMovieCreatorId(id) != this.User.GetId())
            {
                return Unauthorized();
            }

            var movieData = this.data.Movies.Find(id);

            movieData.Title = movie.Title;
            movieData.Summary = movie.Summary;
            movieData.Image = movie.ImageUrl;
            movieData.Duration = movie.Duration;
            movieData.Audio = movie.Audio;
            this.AddActors(movie, movieData);
            this.AddDirectors(movie, movieData);
            this.AddCountries(movie, movieData);

            this.data.SaveChanges();

            return RedirectToAction(nameof(Details), new { id = id });
        }

        [HttpPost]
        [Authorize]
        public IActionResult AddComment(int id, string comment)
        {

            var movie = this.data.Movies.FirstOrDefault(x => x.Id == id);
            var author = this.data.Authors.FirstOrDefault(x => x.UserId == this.User.GetId());

            if (string.IsNullOrWhiteSpace(comment))
            {
                this.ModelState.AddModelError(nameof(movie.Comments), "The comment must be at least 5 characters long.");
            }

            if (!this.ModelState.IsValid)
            {
                return RedirectToAction(nameof(Details), new { id = id });
            }

            var currComment = new Comment
            {
                Movie = movie,
                Author = author,
                Body = comment,
            };

            movie.Comments.Add(currComment);
            data.SaveChanges();

            return RedirectToAction(nameof(Details), new { id = id });
        }

        [Authorize]
        public IActionResult Delete(int id)
        {
            return View();
        }

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

        private string GetMovieCreatorId(int id)
            => this.data
                .Movies
                .Where(x => x.Id == id)
                .Select(x => x.Creator)
                .FirstOrDefault();

        private MovieFormModel GetEditMovieDetails(int id)
            => this.data
                .Movies
                .Where(x => x.Id == id)
                .Select(x => new MovieFormModel
                {
                    Id = x.Id,
                    Audio = x.Audio,
                    ImageUrl = x.Image,
                    Title = x.Title,
                    Summary = x.Summary,
                    Duration = x.Duration,
                    Genres = x.Genres.Select(g => new MovieGenreViewModel
                    {
                        Id = g.Genre.Id,
                        Type = g.Genre.Type,
                    }).ToList(),
                    Actors = x.Actors.Select(a => new AddActorFormModel
                    {
                        FirstName = a.Actor.FirstName,
                        LastName = a.Actor.LastName,
                    }).ToList(),
                    Directors = x.Directors.Select(d => new AddDirectorFormModel
                    {
                        FirstName = d.Director.FirstName,
                        LastName = d.Director.LastName,
                    }).ToList(),
                    Countries = x.Countries.Select(c => new AddCountryFormModel
                    {
                        Name = c.Country.Name,
                    }).ToList()

                }).FirstOrDefault();
        private void AddActors(MovieFormModel movie, Movie movieData)
        {
            if (movie.Actors != null)
            {
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
            }
        }

        private void AddDirectors(MovieFormModel movie, Movie movieData)
        {
            if (movie.Directors != null)
            {
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
            }

        }

        private void AddCountries(MovieFormModel movie, Movie movieData)
        {
            if (movie.Countries != null)
            {
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
            }
        }
    }
}
