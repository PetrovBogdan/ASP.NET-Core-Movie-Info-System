namespace MovieInfoSystem.Services.Movies.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static Data.DataConstants;
    public class MovieServiceModel
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
        public int Duration { get; set; }

        public ICollection<int?> GenreId { get; set; }

        public ICollection<MovieGenreServiceModel> Genres { get; set; }

        public ICollection<AddActorServiceModel> Actors { get; set; }

        public ICollection<AddDirectorServiceModel> Directors { get; set; }

        public ICollection<AddCountryServiceModel> Countries { get; set; }
    }
}
