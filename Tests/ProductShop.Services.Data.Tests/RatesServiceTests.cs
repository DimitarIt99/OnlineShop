namespace ProductShop.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using Moq;
    using ProductShop.Data;
    using ProductShop.Data.Common.Repositories;
    using ProductShop.Data.Models;
    using ProductShop.Data.Models.Enums;
    using ProductShop.Data.Repositories;
    using ProductShop.Web.ViewModels.Comments;
    using ProductShop.Web.ViewModels.Orders;
    using ProductShop.Web.ViewModels.Rates;
    using Xunit;

    public class RatesServiceTests
    {
        [Fact]
        public async Task AlreadyRatedTest()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
           .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);
            dbContext.Ratings.Add(new Rating
            {
                ProductId = 1,
                UserId = "Test",
            });
            await dbContext.SaveChangesAsync();
            var repository = new EfDeletableEntityRepository<Rating>(dbContext);
            var service = new RatesService(repository);

            Assert.True(service.AlreadyRated("Test", 1));
            Assert.False(service.AlreadyRated("Wrong", 1));
        }

        [Fact]
        public async Task AddRateAsyncTest()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
           .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);
            await dbContext.SaveChangesAsync();
            var repository = new EfDeletableEntityRepository<Rating>(dbContext);
            var service = new RatesService(repository);

            var newRating = new RateViewModel
            {
                Description = "Test",
                Grade = (int)Grade.VeryGood,
                ProductId = 1,
                UserId = "Test",
            };

            await service.AddRateAsync(newRating);
        }
    }
}
