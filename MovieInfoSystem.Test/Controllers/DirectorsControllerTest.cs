namespace MovieInfoSystem.Test.Controllers
{
    using Xunit;
    using MyTested.AspNetCore.Mvc;
    using MovieInfoSystem.Controllers;

    using static Data.Directors;
    using MovieInfoSystem.Services.Directors.Models;

    public class DirectorsControllerTest
    {

        [Theory]
        [InlineData(5, null)]
        public void DirectorsAllShouldReturnViewWithCorrectModelAndData(int currentPage,
            string searchTerm)
            => MyMvc
                 .Pipeline()
                 .ShouldMap($"/Directors/All?currentPage={currentPage}")
                 .To<DirectorsController>(x => x.All(currentPage, searchTerm))
                 .Which(controler => controler
                     .WithData(GetTenDirectors))
                 .ShouldReturn()
                 .View(view => view
                 .WithModelOfType<AllDirectorsServiceModel>());

    }
}
