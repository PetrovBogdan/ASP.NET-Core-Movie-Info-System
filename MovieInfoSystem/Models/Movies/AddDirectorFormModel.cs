namespace MovieInfoSystem.Models.Movies
{
    using System.ComponentModel.DataAnnotations;

    using static Data.DataConstants;
    public class AddDirectorFormModel
    {
        [Required]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength)]
        public string LastName { get; set; }

    }
}
