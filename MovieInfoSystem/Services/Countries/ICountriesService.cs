namespace MovieInfoSystem.Services.Countries
{
    using MovieInfoSystem.Services.Countries.Models;
    using System.Collections.Generic;

    public interface ICountriesService
    {
        public List<CountriesListingServiceModel> All();

        public CountryDetailsServiceModel Details(int id);

        public bool AddFlag(string flagUrl, int id);
    }
}
