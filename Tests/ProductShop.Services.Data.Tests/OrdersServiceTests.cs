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
    using Xunit;

    public class OrdersServiceTests
    {
        [Fact]
        public async Task AddOrderAsyncTest()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
           .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);
            dbContext.Products.Add(new Product
            {
                Id = 1,
                Quantity = 2,
            });
            dbContext.ApplicationUsers.Add(new ApplicationUser
            {
                Id = "TestId",
            });
            await dbContext.SaveChangesAsync();
            var repository = new EfDeletableEntityRepository<Order>(dbContext);
            var service = new OrdersService(repository);
            var orderModel = new CreateOrderViewModel
            {
                Adress = "TestAdress",
                ProductId = 1,
                UserId = "TestId",
            };
            await service.AddOrderAsync(orderModel);
            var order = repository.All().Where(a => a.ProductId == 1).FirstOrDefault();
            Assert.True(service.AlreadyOrdered("TestId", 1));
            Assert.Equal(DeliveryState.Ordered, order.State);
            Assert.Equal("TestAdress", order.Adress);
        }

        [Fact]
        public async Task AllOrdersByUserIdTest()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
           .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);
            dbContext.Products.Add(new Product
            {
                Id = 1,
                Quantity = 2,
                Name = "Product1",
                Price = 4,
                ImageUrl = "Image",
                Ratings = new List<Rating>
                {
                    new Rating
                    {
                        Id = 1,
                        ProductId = 1,
                        Grade = Grade.Average,
                    },
                    new Rating
                    {
                        Id = 2,
                        ProductId = 1,
                        Grade = Grade.VeryGood,
                    },
                },
            });
            dbContext.Products.Add(new Product
            {
                Id = 2,
                Quantity = 2,
                Name = "Product2",
                Price = 4,
                ImageUrl = "Image",
                Ratings = new List<Rating>
                {
                    new Rating
                    {
                        Id = 3,
                        ProductId = 2,
                        Grade = Grade.VeryBad,
                    },
                    new Rating
                    {
                        Id = 4,
                        ProductId = 2,
                        Grade = Grade.VeryGood,
                    },
                },
            });
            dbContext.Products.Add(new Product
            {
                Id = 3,
                Quantity = 2,
                Name = "Product3",
                Price = 4,
                ImageUrl = "Image",
                Ratings = new List<Rating>
                {
                    new Rating
                    {
                        Id = 5,
                        ProductId = 3,
                        Grade = Grade.VeryBad,
                    },
                    new Rating
                    {
                        Id = 6,
                        ProductId = 3,
                        Grade = Grade.VeryGood,
                    },
                },
            });
            dbContext.ApplicationUsers.Add(new ApplicationUser
            {
                Id = "TestId",
            });
            dbContext.ApplicationUsers.Add(new ApplicationUser
            {
                Id = "TestId2",
            });
            dbContext.Orders.Add(new Order
            {
                Id = Guid.NewGuid().ToString(),
                ProductId = 1,
                UserId = "TestId",
            });
            dbContext.Orders.Add(new Order
            {
                Id = Guid.NewGuid().ToString(),
                ProductId = 1,
                UserId = "TestId",
            });
            dbContext.Orders.Add(new Order
            {
                Id = Guid.NewGuid().ToString(),
                ProductId = 2,
                UserId = "TestId2",
            });
            await dbContext.SaveChangesAsync();
            var repository = new EfDeletableEntityRepository<Order>(dbContext);
            var service = new OrdersService(repository);

            var orders = service.AllOrdersByUserId("TestId", 10, 0);
            Assert.Equal(2, orders.Count());
            Assert.Equal(4, orders.Where(a => a.ProductId == 1).First().AverageRating);
        }

        [Fact]
        public async Task AlreadyOrderedTest()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
           .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);
            dbContext.Orders.Add(new Order { UserId = "Test", ProductId = 1 });
            await dbContext.SaveChangesAsync();
            var repository = new EfDeletableEntityRepository<Order>(dbContext);
            var service = new OrdersService(repository);
            Assert.True(service.AlreadyOrdered("Test", 1));
            Assert.False(service.AlreadyOrdered("Doenst exist", 10));
        }

        [Fact]
        public async Task CancellAsyncTest()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
           .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);
            dbContext.Products.Add(new Product { Id = 1 });
            dbContext.Orders.Add(new Order { Id = "First order", UserId = "Test", ProductId = 1 });
            await dbContext.SaveChangesAsync();
            var repository = new EfDeletableEntityRepository<Order>(dbContext);
            var service = new OrdersService(repository);

            var deleteOrderModel = new RemoveOrderViewModel { Id = "First order" };

            var productId = await service.CancelAsync(deleteOrderModel);
            Assert.Equal(1, productId);
            Assert.Equal(0, repository.All().Where(a => a.Id == "First order").Count());
        }

        [Fact]
        public async Task ChangeStateAsyncTest()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
           .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);
            dbContext.Orders.Add(new Order
            {
                Id = "First order",
                UserId = "Test",
                ProductId = 1,
                State = DeliveryState.Ordered,
            });
            await dbContext.SaveChangesAsync();
            var repository = new EfDeletableEntityRepository<Order>(dbContext);
            var service = new OrdersService(repository);

            var newState = new EditStateViewModel
            {
                Id = "First order",
                NewState = (int)DeliveryState.OnRoute,
            };
            await service.ChangeStateAsync(newState);
            var order = repository.All()
                .Where(a => a.Id == "First order")
                .FirstOrDefault();
            Assert.Equal(DeliveryState.OnRoute, order.State);
        }

        [Fact]
        public async Task ChangeStateAsyncDeletesOrdersTest()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
           .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);
            dbContext.Orders.Add(new Order
            {
                Id = "First order",
                UserId = "Test",
                ProductId = 1,
                State = DeliveryState.OnTheAdress,
            });
            await dbContext.SaveChangesAsync();
            var repository = new EfDeletableEntityRepository<Order>(dbContext);
            var service = new OrdersService(repository);

            var newState = new EditStateViewModel
            {
                Id = "First order",
                NewState = (int)DeliveryState.Received,
            };
            await service.ChangeStateAsync(newState);
            var orderIsGetsDeleted = repository.All().Any(a => a.Id == "First order");
            var orderIsStillInDb = repository.AllWithDeleted().Any(a => a.Id == "First order");
            Assert.True(orderIsStillInDb);
            Assert.False(orderIsGetsDeleted);
        }

        [Fact]
        public async Task GetOrderDelivaryState()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
           .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);
            dbContext.Orders.Add(new Order
            {
                Id = "First order",
                UserId = "Test",
                ProductId = 1,
                State = DeliveryState.OnTheAdress,
            });
            await dbContext.SaveChangesAsync();
            var repository = new EfDeletableEntityRepository<Order>(dbContext);
            var service = new OrdersService(repository);
            var state = service.GetOrderDelivaryState("First order");

            Assert.Equal("On The Adress", state.StateSpacedOut);
            Assert.Equal(DeliveryState.OnTheAdress, state.State);
        }

        [Fact]
        public async Task IdExistsTest()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
           .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);
            dbContext.Orders.Add(new Order
            {
                Id = "First order",
            });
            await dbContext.SaveChangesAsync();
            var repository = new EfDeletableEntityRepository<Order>(dbContext);
            var service = new OrdersService(repository);

            Assert.True(service.IdExists("First order"));
            Assert.False(service.IdExists("Second order"));
        }

        [Fact]
        public async Task OrderedProductsCountTest()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
           .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);
            dbContext.Orders.Add(new Order
            {
                UserId = "Test",
            });
            dbContext.Orders.Add(new Order
            {
                UserId = "Test",
            });
            dbContext.Orders.Add(new Order
            {
                UserId = "Test",
            });
            dbContext.Orders.Add(new Order
            {
                UserId = "Wrong",
            });
            await dbContext.SaveChangesAsync();
            var repository = new EfDeletableEntityRepository<Order>(dbContext);
            var service = new OrdersService(repository);

            Assert.Equal(3, service.OrderedProductsCount("Test"));
        }
    }
}
