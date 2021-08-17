namespace MovieInfoSystem.Services.Movies
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    using MovieInfoSystem.Data;
    using MovieInfoSystem.Data.Models;
    using MovieInfoSystem.Services.Movies.Models;

    public class MovieService : IMovieService
    {
        private readonly ApplicationDbContext data;

        public MovieService(ApplicationDbContext data)
            => this.data = data;

        public void Create(string title,
            string summary,
            string duration,
            string image,
            string audio,
            int authorId,
            string creator,
            ICollection<int?> genres,
            ICollection<AddActorServiceModel> actors,
            ICollection<AddDirectorServiceModel> directors,
            ICollection<AddCountryServiceModel> countries)
        {
            var movieData = new Movie
            {
                Title = title,
                Summary = summary,
                Duration = duration,
                Image = image,
                Audio = audio,
                AuthorId = authorId,
                Creator = creator,
            };

            foreach (var genreId in genres)
            {
                var genre = this.data.Genres.First(x => x.Id == genreId);

                movieData.Genres.Add(new GenreMovie
                {
                    GenreId = genreId.Value,
                    Genre = genre,
                });
            }

            this.AddActors(actors, movieData);
            this.AddDirectors(directors, movieData);
            this.AddCountries(countries, movieData);

            this.data.Movies.Add(movieData);
            this.data.SaveChanges();
        }

        public MovieDetailsServiceModel Details(int id,
            string userId)
        {
            var movie = this.data
                .Movies
                .Where(x => x.Id == id)
                .Select(x => new MovieDetailsServiceModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    Audio = x.Audio,
                    Image = x.Image,
                    Summary = x.Summary,
                    AuthorId = x.AuthorId,
                    CreatedOn = x.CreatedOn.ToString("dddd, dd MMMM yyyy"),
                    Duration = x.Duration,
                    IsCreator = x.Creator == userId,
                    Comments = x.Comments.Select(c => new MovieCommentsServiceModel
                    {
                        Id = c.Id,
                        AuthorName = c.Author.Name,
                        Body = c.Body,
                        CreatedOn = c.CreatedOn.ToString("dddd, dd MMMM yyyy")
                    }).ToList(),
                    Genres = x.Genres.Select(g => new MovieGenreServiceModel
                    {
                        Id = g.Genre.Id,
                        Type = g.Genre.Type,

                    }).ToList(),
                    Actors = x.Actors.Select(a => new MovieActorsServiceModel
                    {
                        Id = a.Actor.Id,
                        FirstName = a.Actor.FirstName,
                        LastName = a.Actor.LastName,
                    }).ToList(),
                    Directors = x.Directors.Select(d => new MovieDirectorsServiceModel
                    {
                        Id = d.Director.Id,
                        FirstName = d.Director.FirstName,
                        LastName = d.Director.LastName,
                    }).ToList(),
                    Countries = x.Countries.Select(c => new MovieCountriesServiceModel
                    {
                        Id = c.Country.Id,
                        Name = c.Country.Name,
                    }).ToList()
                })
                .FirstOrDefault();

            return movie;
        }

        public void Edit(int id,
            string title,
            string summary,
            string duration,
            string image,
            string audio,
            ICollection<AddActorServiceModel> actors,
            ICollection<AddDirectorServiceModel> directors,
            ICollection<AddCountryServiceModel> countries)
        {

            var movieData = this.data.Movies.Find(id);

            movieData.Title = title;
            movieData.Summary = summary;
            movieData.Image = image;
            movieData.Duration = duration;
            movieData.Audio = audio;
            this.AddActors(actors, movieData);
            this.AddDirectors(directors, movieData);
            this.AddCountries(countries, movieData);

            this.data.SaveChanges();
        }

        public AllMoviesServiceModel All(string searchTerm,
            int currentPage)
        {
            var moviesQuery = this.data.Movies.AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                moviesQuery = moviesQuery
                    .Where(m => m.Title.ToLower().Contains(searchTerm.ToLower()));
            }

            if (currentPage <= 0)
            {
                currentPage = 1;
            }
            var totalMovies = this.data.Movies.Count();

            var maxPage = Math.Ceiling((double)totalMovies / AllMoviesServiceModel.MoviesPerPage);

            if (currentPage > maxPage)
            {
                currentPage = (int)maxPage;
            }

            List<MovieListingServiceModel> movies = new List<MovieListingServiceModel>();

            if (moviesQuery.Count() > 0)
            {
                movies = moviesQuery
                     .OrderByDescending(x => x.Id)
                     .Skip((currentPage - 1) * AllMoviesServiceModel.MoviesPerPage)
                     .Take(AllMoviesServiceModel.MoviesPerPage)
                     .Select(x => new MovieListingServiceModel
                     {
                         Id = x.Id,
                         Title = x.Title,
                         Audio = x.Audio,
                         Image = x.Image,
                         Summary = x.Summary,
                         Actors = x.Actors.Select(a => new MovieActorsServiceModel
                         {
                             Id = a.Actor.Id,
                             FirstName = a.Actor.FirstName,
                             LastName = a.Actor.LastName,

                         }).ToList(),
                         Directors = x.Directors.Select(d => new MovieDirectorsServiceModel
                         {
                             Id = d.Director.Id,
                             FirstName = d.Director.FirstName,
                             LastName = d.Director.LastName,
                         }).ToList(),
                         Countries = x.Countries.Select(c => new MovieCountriesServiceModel
                         {
                             Id = c.Country.Id,
                             Name = c.Country.Name
                         }).ToList()

                     })
                     .ToList();

            }

            return new AllMoviesServiceModel
            {
                TotalMovies = totalMovies,
                CurrentPage = currentPage,
                SearchTerm = searchTerm,
                Movies = movies
            };
        }

        public ICollection<MovieListingServiceModel> Mine(string userId)
        {
            var movies = this.data
                .Movies
                .Where(x => x.Creator == userId)
                 .Select(x => new MovieListingServiceModel
                 {
                     Id = x.Id,
                     Title = x.Title,
                     Audio = x.Audio,
                     Image = x.Image,
                     Summary = x.Summary,
                     Actors = x.Actors.Select(a => new MovieActorsServiceModel
                     {
                         Id = a.Actor.Id,
                         FirstName = a.Actor.FirstName,
                         LastName = a.Actor.LastName,

                     }).ToList(),
                     Directors = x.Directors.Select(d => new MovieDirectorsServiceModel
                     {
                         Id = d.Director.Id,
                         FirstName = d.Director.FirstName,
                         LastName = d.Director.LastName,
                     }).ToList(),
                     Countries = x.Countries.Select(c => new MovieCountriesServiceModel
                     {
                         Id = c.Country.Id,
                         Name = c.Country.Name
                     }).ToList()

                 })
                .ToList();

            return movies;
        }

        public void AddComment(int id, string comment, string userId)
        {
            var movie = this.data.Movies.FirstOrDefault(x => x.Id == id);
            var author = this.data.Authors.FirstOrDefault(x => x.UserId == userId);

            var currComment = new Comment
            {
                Movie = movie,
                Author = author,
                Body = comment,
            };

            movie.Comments.Add(currComment);
            data.SaveChanges();
        }

        public bool Delete(int id)
        {
            var movie = this.data
                .Movies
                .FirstOrDefault(x => x.Id == id);

            if (movie == null)
            {
                return false;
            }

            this.data.Movies.Remove(movie);

            this.data.SaveChanges();

            return true;
        }

        public ICollection<MovieGenreServiceModel> GetGenres()
            => this.data
            .Genres
            .Select(x => new MovieGenreServiceModel
            {
                Id = x.Id,
                Type = x.Type,
            })
            .OrderBy(x => x.Type)
            .ToList();

        public string GetCreatorId(int userId)
             => this.data
                      .Movies
                      .Where(x => x.Id == userId)
                      .Select(x => x.Creator)
                      .FirstOrDefault();


        public MovieServiceModel GetEditDetails(int id)
            => this.data
                .Movies
                .Where(x => x.Id == id)
                .Select(x => new MovieServiceModel
                {
                    Id = x.Id,
                    Audio = x.Audio,
                    ImageUrl = x.Image,
                    Title = x.Title,
                    Summary = x.Summary,
                    Duration = x.Duration,
                    Genres = x.Genres.Select(g => new MovieGenreServiceModel
                    {
                        Id = g.Genre.Id,
                        Type = g.Genre.Type,
                    }).ToList(),
                    Actors = x.Actors.Select(a => new AddActorServiceModel
                    {
                        FirstName = a.Actor.FirstName,
                        LastName = a.Actor.LastName,
                    }).ToList(),
                    Directors = x.Directors.Select(d => new AddDirectorServiceModel
                    {
                        FirstName = d.Director.FirstName,
                        LastName = d.Director.LastName,
                    }).ToList(),
                    Countries = x.Countries.Select(c => new AddCountryServiceModel
                    {
                        Name = c.Country.Name,
                    }).ToList()

                }).FirstOrDefault();

        private void AddActors(ICollection<AddActorServiceModel> actors,
            Movie movieData)
        {
            if (actors != null)
            {
                foreach (var actor in actors.Where(x => x.FirstName != null || x.LastName != null))
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

        private void AddDirectors(ICollection<AddDirectorServiceModel> directors,
            Movie movieData)
        {
            if (directors != null)
            {
                foreach (var director in directors.Where(x => x.FirstName != null || x.LastName != null))
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

        private void AddCountries(ICollection<AddCountryServiceModel> countries, 
            Movie movieData)
        {
            if (countries != null)
            {
                foreach (var country in countries.Where(x => x.Name != null))
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
