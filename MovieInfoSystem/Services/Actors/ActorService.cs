﻿namespace MovieInfoSystem.Services.Actors
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using MovieInfoSystem.Data;
    using MovieInfoSystem.Data.Models;
    using MovieInfoSystem.Services.Actors.Models;

    public class ActorService : IActorService
    {
        private readonly ApplicationDbContext data;

        public ActorService(ApplicationDbContext data)
            => this.data = data;

        public AllActorsServiceModel All(int currentPage,
            string searchTerm)
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

            var maxPage = Math.Ceiling((double)totalActors / AllActorsServiceModel.ActorsPerPage);

            if (currentPage > maxPage)
            {
                currentPage = (int)maxPage;
            }

            List<ActorsListingServiceModel> actors = new List<ActorsListingServiceModel>();

            if (actorsQuery.Count() > 1)
            {
                actors = actorsQuery
                     .OrderByDescending(x => x.Id)
                     .Skip((currentPage - 1) * AllActorsServiceModel.ActorsPerPage)
                     .Take(AllActorsServiceModel.ActorsPerPage)
                     .Select(x => new ActorsListingServiceModel
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

            return new AllActorsServiceModel
            {
                TotalActors = totalActors,
                CurrentPage = currentPage,
                SearchTerm = searchTerm,
                Actors = actors,
            };
        }

        public ActorDetailsServiceModel Details(int id)
            => this.data
                .Actors
                .Where(x => x.Id == id)
                .Select(x => new ActorDetailsServiceModel
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


        public bool AddDetails(string country,
            string biography,
            string picture,
            int id)
        {
            var actor = this.data
               .Actors
               .Where(x => x.Id == id)
               .FirstOrDefault();

            if (actor == null)
            {
                return false;
            }

            actor.Biography = biography;
            actor.Picture = picture;

            var countryData = this.data
                .Countries
                .Where(x => x.Name == country)
                .FirstOrDefault();

            if (countryData == null)
            {
                countryData = new Country
                {
                    Name = country,
                };
            }

            actor.Country = countryData;

            this.data.SaveChanges();

            return true;
        }


    }
}