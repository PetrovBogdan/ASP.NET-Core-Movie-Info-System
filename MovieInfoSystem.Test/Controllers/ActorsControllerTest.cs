namespace MovieInfoSystem.Test.Controllers
{
    using Xunit;
    using MyTested.AspNetCore.Mvc;
    using MovieInfoSystem.Controllers;
    using MovieInfoSystem.Services.Actors.Models;

    using static Data.Actors;

    public class ActorsControllerTest
    {
        [Theory]
        [InlineData(5, null)]
        public void ActorsAllShouldReturnViewWithCorrectModelAndData(int currentPage,
        string searchTerm)
        => MyMvc
             .Pipeline()
             .ShouldMap($"/Actors/All?currentPage={currentPage}")
             .To<ActorsController>(x => x.All(currentPage, searchTerm))
             .Which(controler => controler
                 .WithData(GetTenActors))
             .ShouldReturn()
             .View(view => view
             .WithModelOfType<AllActorsServiceModel>());

    }
}
