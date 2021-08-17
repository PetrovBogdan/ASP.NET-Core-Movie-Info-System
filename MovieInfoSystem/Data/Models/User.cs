namespace MovieInfoSystem.Data.Models
{
    using Microsoft.AspNetCore.Identity;
    using System.ComponentModel.DataAnnotations;

    using static Data.DataConstants;
    public class User : IdentityUser
    {
        [StringLength(FullNameMaxLength)]
        public string FullName { get; set; }

    }
}
