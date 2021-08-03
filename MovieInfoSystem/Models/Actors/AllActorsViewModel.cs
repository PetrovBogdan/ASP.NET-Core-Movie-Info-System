namespace MovieInfoSystem.Models.Actors
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class AllActorsViewModel
    {
        public const int ActorsPerPage = 4;

        [Display(Name = "Search by actor name")]
        public string SearchTerm { get; set; }

        public int CurrentPage { get; set; } = 1;

        public int TotalActors { get; set; }

        public List<ActorsListingViewModel> Actors { get; init; }
    }
}
