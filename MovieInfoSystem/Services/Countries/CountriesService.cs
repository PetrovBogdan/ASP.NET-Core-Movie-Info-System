namespace MovieInfoSystem.Services.Countries
{
    using System.Linq;
    using System.Collections.Generic;

    using MovieInfoSystem.Data;
    using MovieInfoSystem.Services.Countries.Models;

    public class CountriesService : ICountriesService
    {
        private readonly ApplicationDbContext data;

        public CountriesService(ApplicationDbContext data)
            => this.data = data;

        public bool AddFlag(string flagUrl,
            int id)
        {
            var country = this.data
                .Countries
                .FirstOrDefault(x => x.Id == id);

            if (country == null)
            {
                return false;
            }

            country.FlagUrl = flagUrl;

            this.data.SaveChanges();

            return true;
        }

        public List<CountriesListingServiceModel> All()
            => this.data
                .Countries
                .Select(x => new CountriesListingServiceModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Flag = x.FlagUrl,
                    Actors = x.Actors.Count(),
                    Directors = x.Directors.Count(),
                    Movies = x.Movies.Count(),
                }).ToList();

        public CountryDetailsServiceModel Details(int id)
            => this.data
            .Countries
            .Where(x => x.Id == id)
            .Select(x => new CountryDetailsServiceModel
            {
                Id = x.Id,
                Flag = x.FlagUrl,
                Name = x.Name,
                Actors = x.Actors.Select(a => new ActorCountryServiceModel
                {
                    Id = a.Id,
                    FirstName = a.FirstName,
                    LastName = a.LastName,
                }).ToList(),
                Directors = x.Directors.Select(d => new DirectorCountryServiceModel
                {
                    Id = d.Id,
                    FirstName = d.FirstName,
                    LastName = d.LastName,
                }).ToList(),
                Movies = x.Movies.Select(m => new MovieCountryServiceModel
                {
                    Id = m.Movie.Id,
                    Name = m.Movie.Title,
                }).ToList(),

            }).FirstOrDefault();
    }
}
