namespace MovieInfoSystem.Services.Actors.Models
{
    using System.Collections.Generic;

    using System.ComponentModel.DataAnnotations;

    public class AllActorsServiceModel
    {
        public const int ActorsPerPage = 4;

        [Display(Name = "Search by actor name")]
        public string SearchTerm { get; set; }

        public int CurrentPage { get; set; } = 1;

        public int TotalActors { get; set; }

        public List<ActorsListingServiceModel> Actors { get; init; }
    }
}
