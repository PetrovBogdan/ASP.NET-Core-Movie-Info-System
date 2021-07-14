namespace MyWebProjectDb.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Comment
    {
        [Key]
        public int Id { get; init; }

        [Required]
        public string Description { get; set; }

        public DateTime CreatedOn { get; init; } = DateTime.UtcNow;

        public int Likes { get; set; }

        public int Dislikes { get; set; }

    }
}
