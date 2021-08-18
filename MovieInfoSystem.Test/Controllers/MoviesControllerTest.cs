namespace MovieInfoSystem.Test.Controllers
{
    using Xunit;
    using MyTested.AspNetCore.Mvc;
    using MovieInfoSystem.Controllers;
    using MovieInfoSystem.Services.Movies.Models;
   
    using static Data.Movies;

    public class MoviesControllerTest
    {
        [Theory]
        [InlineData(5, null)]
        public void MoviesAllShouldReturnViewWithCorrectModelAndData(int currentPage,
        string searchTerm)
            => MyMvc
            .Pipeline()
            .ShouldMap($"/Movies/All?currentPage={currentPage}")
            .To<MoviesController>(x => x.All(currentPage, searchTerm))
            .Which(controler => controler
                .WithData(GetTenMovies))
            .ShouldReturn()
            .View(view => view
            .WithModelOfType<AllMoviesServiceModel>());
    }
}
