namespace MovieInfoSystem.Services.Movies.Models
{
    using System.ComponentModel.DataAnnotations;

    using static Data.DataConstants;
    public class AddActorServiceModel
    {
        public int ActorId { get; set; }
        
        [StringLength(NameMaxLength, MinimumLength = NameMinLength)]
        public string FirstName { get; set; }

        [StringLength(NameMaxLength, MinimumLength = NameMinLength)]
        public string LastName { get; set; }

    }
}
