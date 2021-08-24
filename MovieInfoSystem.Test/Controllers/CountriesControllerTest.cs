namespace MovieInfoSystem.Test.Controllers
{
    using System.Collections.Generic;

    using Xunit;
    using MyTested.AspNetCore.Mvc;
    using MovieInfoSystem.Controllers;
    using MovieInfoSystem.Services.Countries.Models;

    using static Data.Countries;

    public class CountriesControllerTest
    {
        [Fact]
        public void CountriesAllShouldReturnViewWithCorrectModelAndData()
            => MyMvc
                     .Pipeline()
                     .ShouldMap("/Countries/All")
                     .To<CountriesController>(x => x.All())
                     .Which(controler => controler
                         .WithData(GetTenCountries))
                     .ShouldReturn()
                     .View(view => view
                     .WithModelOfType<List<CountriesListingServiceModel>>());

    }
}
