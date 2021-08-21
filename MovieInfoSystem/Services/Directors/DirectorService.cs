namespace MovieInfoSystem.Services.Directors
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    using MovieInfoSystem.Data;
    using MovieInfoSystem.Services.Directors.Models;
    using MovieInfoSystem.Data.Models;

    public class DirectorService : IDirectorService
    {
        private readonly ApplicationDbContext data;

        public DirectorService(ApplicationDbContext data)
            => this.data = data;

        public AllDirectorsServiceModel All(int currentPage,
            string searchTerm)
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

            var maxPage = Math.Ceiling((double)totalDirectors / AllDirectorsServiceModel.DirectorsPerPage);

            if (currentPage > maxPage)
            {
                currentPage = (int)maxPage;
            }

            List<DirectorsListingServiceModel> directors = new List<DirectorsListingServiceModel>();

            if (directorsQuery.Count() > 0)
            {
                directors = directorsQuery
                      .OrderByDescending(x => x.Id)
                      .Skip((currentPage - 1) * AllDirectorsServiceModel.DirectorsPerPage)
                      .Take(AllDirectorsServiceModel.DirectorsPerPage)
                      .Select(x => new DirectorsListingServiceModel
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

            return new AllDirectorsServiceModel
            {
                TotalDirectors = totalDirectors,
                CurrentPage = currentPage,
                SearchTerm = searchTerm,
                Directors = directors,
            };
        }

        public DirectorDetailsServiceModel Details(int id)
            => this.data
                .Directors
                .Where(x => x.Id == id)
                .Select(x => new DirectorDetailsServiceModel
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Country = new DirectorCountryServiceModel
                    { Id = x.Country.Id, Name = x.Country.Name },
                    Picture = x.Picture,
                    Biography = x.Biography,
                    Movies = x.Movies.Select(m => new DirectorMovieServiceModel
                    {
                        Id = m.Movie.Id,
                        Name = m.Movie.Title,
                    }).ToList(),
                })
                .FirstOrDefault();


        public bool AddDetails(string country,
            string biography,
            string picture,
            int id)
        {
            var director = this.data
               .Directors
               .Where(x => x.Id == id)
               .FirstOrDefault();

            if (director == null)
            {
                return false;
            }

            director.Biography = biography;
            director.Picture = picture;

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

            director.Country = countryData;

            this.data.SaveChanges();

            return true;
        }
    }
}
