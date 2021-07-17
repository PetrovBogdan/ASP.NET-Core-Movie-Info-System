namespace MovieInfoSystem.Data
{
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

            builder
                .Entity<ActorMovie>()
                .HasOne(x => x.Actor)
                .WithMany(x => x.Movies)
                .HasForeignKey(x => x.ActorId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder
             .Entity<ActorMovie>()
             .HasOne(x => x.Movie)
             .WithMany(x => x.Actors)
             .HasForeignKey(x => x.MovieId)
             .IsRequired()
             .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<DirectorMovie>()
                .HasOne(x => x.Movie)
                .WithMany(x => x.Directors)
                .HasForeignKey(x => x.MovieId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<DirectorMovie>()
                .HasOne(x => x.Director)
                .WithMany(x => x.Movies)
                .HasForeignKey(x => x.DirectorId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<GenreMovie>()
                .HasOne(x => x.Genre)
                .WithMany(x => x.Movies)
                .HasForeignKey(x => x.GenreId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<GenreMovie>()
                .HasOne(x => x.Movie)
                .WithMany(x => x.Genres)
                .HasForeignKey(x => x.MovieId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder
              .Entity<CountryMovie>()
              .HasOne(x => x.Country)
              .WithMany(x => x.Movies)
              .HasForeignKey(x => x.CountryId)
              .IsRequired()
              .OnDelete(DeleteBehavior.Restrict);

            builder
               .Entity<CountryMovie>()
               .HasOne(x => x.Movie)
               .WithMany(x => x.Countries)
               .HasForeignKey(x => x.MovieId)
               .IsRequired()
               .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(builder);
        }
    }
}
