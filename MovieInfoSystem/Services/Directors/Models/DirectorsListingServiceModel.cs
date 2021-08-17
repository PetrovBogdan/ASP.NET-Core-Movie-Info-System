namespace MovieInfoSystem.Services.Directors.Models
{
    using System.Collections.Generic;

    public class DirectorsListingServiceModel
    {
        public int Id { get; init; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Country { get; set; }

        public string Biography { get; set; }

        public string Picture { get; set; }

        public ICollection<string> Movies { get; init; }
    }
}
