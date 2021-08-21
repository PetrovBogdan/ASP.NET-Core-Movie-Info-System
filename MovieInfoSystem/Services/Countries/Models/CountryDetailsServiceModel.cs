namespace MovieInfoSystem.Services.Countries.Models
{
    using System.Collections.Generic;

    public class CountryDetailsServiceModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Flag { get; set; }

        public ICollection<ActorCountryServiceModel> Actors { get; set; }

        public ICollection<DirectorCountryServiceModel> Directors { get; set; }

        public ICollection<MovieCountryServiceModel> Movies { get; set; }

    }
}
