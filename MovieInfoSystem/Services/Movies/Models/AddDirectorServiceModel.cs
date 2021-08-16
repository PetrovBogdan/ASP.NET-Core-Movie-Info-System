namespace MovieInfoSystem.Services.Movies.Models
{
    using System.ComponentModel.DataAnnotations;

    using static Data.DataConstants;
    public class AddDirectorServiceModel
    {
        [Required]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength)]
        public string LastName { get; set; }
    }
}
