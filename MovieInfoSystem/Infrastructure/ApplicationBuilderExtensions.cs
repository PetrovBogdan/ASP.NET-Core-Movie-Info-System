namespace MovieInfoSystem.Infrastructure
{
    using System.Linq;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using MovieInfoSystem.Data;
    using MyWebProjectDb.Data.Models;

    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder PrepareDatabase
            (this IApplicationBuilder app)
        {
            var scopedServices = app.ApplicationServices.CreateScope();

            var data = scopedServices.ServiceProvider.GetService<ApplicationDbContext>();

            data.Database.Migrate();

            SeedGenres(data);

            return app;
        }

        private static void SeedGenres(ApplicationDbContext data)
        {
            if (data.Genres.Any())
            {
                return;
            }

            data.Genres.AddRange(new[]
            {
                new Genre {Type = "Horor"},
                new Genre{Type = "Comedy"},
                new Genre{Type = "Action"},
                new Genre{Type = "Drama"},
                new Genre{Type = "Fantasy"},
                new Genre{Type = "Horror"},
                new Genre{Type = "Mystery"},
                new Genre{Type = "Romance"},
                new Genre{Type = "Thriller"},
                new Genre{Type = "Adventure"},
                new Genre{Type = "Historical"},
                new Genre{Type = "Science fiction"},
                new Genre{Type = "Animation"},
                new Genre{Type = "Musical"},
                new Genre{Type = "Crime"},
            });
        }

    }
}
