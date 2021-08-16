namespace MovieInfoSystem.Services.Authors
{
    using System.Linq;
    using MovieInfoSystem.Data;

    public class AuthorService : IAuthorService
    {
        private readonly ApplicationDbContext data;

        public AuthorService(ApplicationDbContext data)
            => this.data = data;

        public int GetId(string userId)
            => this.data
                .Authors
                .Where(x => x.UserId == userId)
                .Select(x => x.Id)
                .FirstOrDefault();

        public bool IsAuthor(string userId)
            => this.data
                .Authors
                .Any(x => x.UserId == userId);
    }
}
