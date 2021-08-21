namespace MovieInfoSystem.Services.Directors.Models
{
    using System.Collections.Generic;

    public class DirectorDetailsServiceModel
    {
        public int Id { get; init; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DirectorCountryServiceModel Country { get; set; }

        public string Biography { get; set; }

        public string Picture { get; set; }

        public ICollection<DirectorMovieServiceModel> Movies { get; init; }
    }
}
