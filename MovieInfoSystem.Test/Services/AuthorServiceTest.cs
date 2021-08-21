namespace MovieInfoSystem.Test.Services
{
    using Xunit;
    using MovieInfoSystem.Test.Mocks;
    using MovieInfoSystem.Data.Models;
    using MovieInfoSystem.Services.Authors;

    public class AuthorServiceTest
    {
        [Fact]
        public void IsAuthorShoudReturnTrueWhenTheGivenIdExists()
        {
            //Arrange
            const string userId = "Test";
            using var data = DatabaseMock.Instance;

            data.Authors.Add(new Author { UserId = userId });
            data.SaveChanges();
            
            var authorService = new AuthorService(data);

            //Act
            var result = authorService.IsAuthor(userId);

            //Assert
            Assert.True(result);
        }

        [Fact]
        public void IsAuthorShoudReturnFalseWhenTheGivenIdDoesntExist()
        {
            //Arrange
            using var data = DatabaseMock.Instance;

            data.Authors.Add(new Author { UserId = "test" });
            data.SaveChanges();

            var authorService = new AuthorService(data);

            //Act
            var result = authorService.IsAuthor("New");

            //Assert
            Assert.False(result);
        }
    }
}
