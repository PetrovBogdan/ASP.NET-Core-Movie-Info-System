namespace MovieInfoSystem.Models.Movies
{
    using System;
    using System.Configuration;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static Data.DataConstants;
    public class AddMovieFormModel
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
        [TimeSpanValidator(MinValueString = "00:01:41", MaxValueString = "21:00:0", ExcludeRange = false)]
        public TimeSpan Duration { get; set; }

        [Required]
        public ICollection<int?> GenreId { get; set; }

        public ICollection<MovieGenreViewModel> Genres { get; set; }

        [Required]
        public ICollection<AddActorFormModel> Actors { get; set; }

        [Required]
        public ICollection<AddDirectorFormModel> Directors { get; set; }

        [Required]
        public ICollection<AddCountryFormModel> Countries { get; set; }

    }
}
