namespace MovieInfoSystem.Models.Movies
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static Data.DataConstants;
    public class MovieFormModel
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        [Url]
        [Display(Name = "Image URL")]
        public string ImageUrl { get; set; }

        [Required]
        [StringLength(AudioMaxLength, MinimumLength = AudioMinLength)]
        public string Audio { get; set; }

        [Required]
        [StringLength(SummaryMaxLength, MinimumLength = SummaryMinLength)]
        public string Summary { get; set; }

        [Required]
        [Display(Name = "Duration in minutes")]
        public string Duration { get; set; }

        public ICollection<int?> GenreId { get; set; }

        public ICollection<MovieGenreViewModel> Genres { get; set; }

        public ICollection<AddActorFormModel> Actors { get; set; }

        public ICollection<AddDirectorFormModel> Directors { get; set; }

        public ICollection<AddCountryFormModel> Countries { get; set; }

    }
}
