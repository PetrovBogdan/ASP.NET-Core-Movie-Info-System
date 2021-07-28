namespace MovieInfoSystem.Data
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using MovieInfoSystem.Data.Models;

    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Movie> Movies { get; set; }

        public DbSet<Actor> Actors { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<Country> Countries { get; set; }

        public DbSet<Director> Directors { get; set; }

        public DbSet<Genre> Genres { get; set; }

        public DbSet<Author> Authors { get; set; }

        public DbSet<ActorMovie> ActorMovie { get; set; }

        public DbSet<GenreMovie> GenreMovie { get; set; }

        public DbSet<CountryMovie> CountryMovie { get; set; }

        public DbSet<DirectorMovie> DirectorMOvie { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<ActorMovie>()
                .HasKey(x => new { x.ActorId, x.MovieId });

            builder
                .Entity<DirectorMovie>()
                .HasKey(x => new { x.DirectorId, x.MovieId });

            builder
                .Entity<GenreMovie>()
                .HasKey(x => new { x.GenreId, x.MovieId });

            builder
                .Entity<CountryMovie>()
                .HasKey(x => new { x.CountryId, x.MovieId });

            builder.Entity<Author>()
                .HasOne<IdentityUser>()
                .WithOne()
                .HasForeignKey<Author>(x => x.UserId);

            builder.Entity<Movie>()
                .HasOne(x => x.Author)
                .WithMany(x => x.Movies)
                .HasForeignKey(x => x.AuthorId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(builder);
        }
    }
}
