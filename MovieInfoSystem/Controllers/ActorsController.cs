namespace MovieInfoSystem.Controllers
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    using MovieInfoSystem.Data;
    using Microsoft.AspNetCore.Mvc;
    using MovieInfoSystem.Data.Models;
    using MovieInfoSystem.Models.Actors;
    using MovieInfoSystem.Infrastructure;
    using MovieInfoSystem.Services.Authors;
    using Microsoft.AspNetCore.Authorization;

    public class ActorsController : Controller
    {
        private readonly ApplicationDbContext data;
        private readonly IAuthorService authorService;

        public ActorsController(ApplicationDbContext data, IAuthorService authorService)
        {
            this.data = data;
            this.authorService = authorService;
        }

        public IActionResult All(int currentPage, string searchTerm)
        {
            var actorsQuery = this.data.Actors.AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                actorsQuery = actorsQuery
                   .Where(x => x.FirstName.ToLower().Contains(searchTerm.ToLower()) ||
                           x.LastName.ToLower().Contains(searchTerm.ToLower()));
            }

            if (currentPage <= 0)
            {
                currentPage = 1;
            }

            var totalActors = actorsQuery.Count();

            var maxPage = Math.Ceiling((double)totalActors / AllActorsViewModel.ActorsPerPage);

            if (currentPage > maxPage)
            {
                currentPage = (int)maxPage;
            }

            List<ActorsListingViewModel> actors = new List<ActorsListingViewModel>();

            if (actorsQuery.Count() > 1)
            {
                actors = actorsQuery
                     .OrderByDescending(x => x.Id)
                     .Skip((currentPage - 1) * AllActorsViewModel.ActorsPerPage)
                     .Take(AllActorsViewModel.ActorsPerPage)
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
            }

            return View(new AllActorsViewModel
            {
                TotalActors = totalActors,
                CurrentPage = currentPage,
                SearchTerm = searchTerm,
                Actors = actors,
            });
        }

        [Authorize]
        public IActionResult Details(int id)
        {
            var actor = this.data
                .Actors
                .Where(x => x.Id == id)
                .Select(x => new ActorDetailsViewModel
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

            if (actor == null)
            {
                return BadRequest();
            }

            return View(actor);
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
        public IActionResult AddDetails(AddActorDetailsFormModel details, int id)
        {
            if (!this.ModelState.IsValid)
            {
                return View();
            }

            var actor = this.data
                .Actors
                .Where(x => x.Id == id)
                .FirstOrDefault();

            if (actor == null)
            {
                return BadRequest();
            }

            actor.Biography = details.Biography;
            actor.Picture = details.Picture;

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

            actor.Country = country;

            this.data.SaveChanges();

            return RedirectToAction(nameof(Details), new { id = id });
        }

    }
}
