namespace MovieInfoSystem.Services.Authors
{
    using System.Linq;
    using MovieInfoSystem.Data;
    using MovieInfoSystem.Data.Models;

    public class AuthorService : IAuthorService
    {
        private readonly ApplicationDbContext data;

        public AuthorService(ApplicationDbContext data)
            => this.data = data;

        public void Create(string name,
            string email,
            string userId)
        {
            var authorData = new Author
            {
                Name = name,
                Email = email,
                UserId = userId,
            };

            this.data.Authors.Add(authorData);

            this.data.SaveChanges();
        }

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
