namespace MovieInfoSystem.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;
    using MovieInfoSystem.Data;
    using MovieInfoSystem.Models.Movies;

    public class MoviesController : Controller
    {
        private readonly ApplicationDbContext data;

        public MoviesController(ApplicationDbContext data)
            => this.data = data;

        public IActionResult Add() => View(new AddMovieFormModel
        {
            Genres = this.GetMovieGenres(),
        });

        [HttpPost]
        public IActionResult Add(AddMovieFormModel movie)
        {
            if (!ModelState.IsValid)
            {
                movie.Genres = this.GetMovieGenres();
                return View(movie);
            }

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
            .ToList();
    }
}
