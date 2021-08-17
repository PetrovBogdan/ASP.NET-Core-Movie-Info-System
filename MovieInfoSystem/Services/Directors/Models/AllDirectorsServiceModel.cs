namespace MovieInfoSystem.Services.Directors.Models
{
    using System.Collections.Generic;

    using System.ComponentModel.DataAnnotations;

    public class AllDirectorsServiceModel
    {
        public const int DirectorsPerPage = 4;

        [Display(Name = "Search by director name")]
        public string SearchTerm { get; set; }

        public int CurrentPage { get; set; } = 1;

        public int TotalDirectors { get; set; }

        public List<DirectorsListingServiceModel> Directors { get; init; }
    }
}
