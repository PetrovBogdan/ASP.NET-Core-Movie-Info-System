namespace MovieInfoSystem.Models.Directors
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class AllDirectorsViewModel
    {
        public const int DirectorsPerPage = 4;

        [Display(Name = "Search by actor name")]
        public string SearchTerm { get; set; }

        public int CurrentPage { get; set; } = 1;

        public int TotalDirectors { get; set; }

        public List<DirectorsListingViewModel> Directors { get; init; }
    }
}
