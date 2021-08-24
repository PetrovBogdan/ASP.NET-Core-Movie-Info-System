namespace MovieInfoSystem.Infrastructure
{
    using System;
    using System.Linq;

    using MovieInfoSystem.Data;
    using System.Threading.Tasks;
    using MovieInfoSystem.Data.Models;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;

    using static WebConstants;
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder PrepareDatabase
            (this IApplicationBuilder app)
        {
            var scopedServices = app.ApplicationServices.CreateScope();
            var serviceProvider = scopedServices.ServiceProvider;

            var data = serviceProvider.GetRequiredService<ApplicationDbContext>();

            data.Database.Migrate();

            SeedGenres(data);
            SeedAdministrator(serviceProvider, data);

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

            data.SaveChanges();
        }

        private static void SeedAdministrator(IServiceProvider services,
            ApplicationDbContext data)
        {
            var userManager = services.GetRequiredService<UserManager<User>>();
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

            Task.Run(async () =>
            {
                if (await roleManager.RoleExistsAsync(AdministratorRoleName))
                {
                    return;
                }

                var role = new IdentityRole { Name = AdministratorRoleName };

                await roleManager.CreateAsync(role);

                const string adminEmail = "admin@mif.com";
                const string adminPassword = "admin666";

                var user = new User
                {
                    Email = adminEmail,
                    UserName = adminEmail,
                    FullName = "Administrator",
                };

                await userManager.CreateAsync(user, adminPassword);

                await userManager.AddToRoleAsync(user, role.Name);

                var authorData = new Author
                {
                    Name = "Admin",
                    Email = adminEmail,
                    UserId = data.Users
                                 .Where(x => x.Email == adminEmail)
                             .FirstOrDefault().Id,
                };

                data.Authors.Add(authorData);

                data.SaveChanges();

            })
              .GetAwaiter()
              .GetResult();
        }
    }
}
