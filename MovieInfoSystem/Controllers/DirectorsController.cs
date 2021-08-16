namespace MovieInfoSystem.Controllers
{
    using System;
    using System.Linq;

    using MovieInfoSystem.Data;
    using Microsoft.AspNetCore.Mvc;
    using MovieInfoSystem.Data.Models;
    using MovieInfoSystem.Infrastructure;
    using MovieInfoSystem.Services.Authors;
    using MovieInfoSystem.Models.Directors;
    using Microsoft.AspNetCore.Authorization;

    public class DirectorsController : Controller
    {
        private readonly ApplicationDbContext data;
        private readonly IAuthorService authorService;

        public DirectorsController(ApplicationDbContext data, IAuthorService authorService)
        {
            this.data = data;
            this.authorService = authorService;
        }

        public IActionResult All(int currentPage, string searchTerm)
        {
            var directorsQuery = this.data.Directors.AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                directorsQuery = directorsQuery
                   .Where(x => x.FirstName.ToLower().Contains(searchTerm.ToLower()) ||
                           x.LastName.ToLower().Contains(searchTerm.ToLower()));
            }

            if (currentPage <= 0)
            {
                currentPage = 1;
            }

            var totalDirectors = this.data.Directors.Count();

            var maxPage = Math.Ceiling((double)totalDirectors / AllDirectorsViewModel.DirectorsPerPage);

            if (currentPage > maxPage)
            {
                currentPage = (int)maxPage;
            }

            var directors = directorsQuery
                .OrderByDescending(x => x.Id)
                .Skip((currentPage - 1) * AllDirectorsViewModel.DirectorsPerPage)
                .Take(AllDirectorsViewModel.DirectorsPerPage)
                .Select(x => new DirectorsListingViewModel
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

            return View(new AllDirectorsViewModel
            {
                TotalDirectors = totalDirectors,
                CurrentPage = currentPage,
                SearchTerm = searchTerm,
                Directors = directors,
            });
        }

        [Authorize]
        public IActionResult Details(int id)
        {
            var director = this.data
                .Directors
                .Where(x => x.Id == id)
                .Select(x => new DirectorDetailsViewModel
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Country = x.Country.Name,
                    Picture = x.Picture,
                    Biography = x.Biography,
                    Movies = x.Movies.Select(x => x.Movie.Title).ToList(),
                })
                .FirstOrDefault();

            if (director == null)
            {
                return BadRequest();
            }

            return View(director);
        }

        [Authorize]
        public IActionResult AddDetails()
        {
            var userId = this.User.GetId();

            if (!this.authorService.IsAuthor(userId))
            {
                return RedirectToAction("Create", "Authors");
            }

            return View();
        }

        [HttpPost]
        [Authorize]
        public IActionResult AddDetails(AddDirectorDetailsFormModel details, int id)
        {
            if (!this.ModelState.IsValid)
            {
                return View();
            }

            var director = this.data
                .Directors
                .Where(x => x.Id == id)
                .FirstOrDefault();

            if (director == null)
            {
                return BadRequest();
            }

            director.Biography = details.Biography;
            director.Picture = details.Picture;

            var country = this.data
                .Countries
                .Where(x => x.Name == details.CountryName)
                .FirstOrDefault();

            if (country == null)
            {
                country = new Country
                {
                    Name = details.CountryName,
                };
            }

            director.Country = country;

            this.data.SaveChanges();

            return RedirectToAction(nameof(Details), new { id = id });
        }
    }
}
