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
    using ProductShop.Data.Repositories;
    using ProductShop.Web.ViewModels.Comments;
    using Xunit;

    public class CommentsServiceTests
    {

        [Fact]
        public void GetCountTest()
        {
            var repository = new Mock<IDeletableEntityRepository<Comment>>();
            repository.Setup(a => a.All()).Returns(new List<Comment>
             {
                 new Comment(),
                 new Comment(),
                 new Comment(),
             }.AsQueryable());
            var service = new CommentsService(repository.Object);
            Assert.Equal(3, service.GetCommentsCount());
        }

        [Fact]
        public async Task CreateCommentTest()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);

            var repository = new EfDeletableEntityRepository<Comment>(dbContext);
            var service = new CommentsService(repository);
            var createCommentViewModel = new CreateCommentsViewModel
            {
                Content = "ssss",
                ProductId = 1,
                UserId = Guid.NewGuid().ToString(),
            };
            await service.CreateCommentAsync(createCommentViewModel);
            Assert.Equal(1, service.GetCommentsCount());
        }

        [Fact]
        public async Task CommentsInMemoryDatabaseTest()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                 .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);
            dbContext.Comments.Add(new Comment());
            dbContext.Comments.Add(new Comment());
            dbContext.Comments.Add(new Comment());
            await dbContext.SaveChangesAsync();

            var repository = new EfDeletableEntityRepository<Comment>(dbContext);
            var service = new CommentsService(repository);
            Assert.Equal(3, service.GetCommentsCount());
        }

        [Fact]
        public async Task DeleteCommentTestWithMoreThenOne()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                 .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);
            dbContext.Comments.Add(new Comment
            {
                Id = 1,
            });
            dbContext.Comments.Add(new Comment
            {
                Id = 3,
            });
            dbContext.Comments.Add(new Comment
            {
                Id = 2,
            });
            await dbContext.SaveChangesAsync();

            var repository = new EfDeletableEntityRepository<Comment>(dbContext);
            var service = new CommentsService(repository);
            await service.DeleteCommentAsync(1);
            Assert.Equal(2, service.GetCommentsCount());
        }

        [Fact]
        public async Task DeleteCommentTestWithJustOne()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                 .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);
            dbContext.Comments.Add(new Comment
            {
                Id = 1,
            });
            await dbContext.SaveChangesAsync();

            var repository = new EfDeletableEntityRepository<Comment>(dbContext);
            var service = new CommentsService(repository);
            await service.DeleteCommentAsync(1);
            Assert.Equal(0, service.GetCommentsCount());
        }

        [Fact]
        public async Task CreatingCommentsTest()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                 .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);

            var repository = new EfDeletableEntityRepository<Comment>(dbContext);
            var service = new CommentsService(repository);

            await service.CreateCommentAsync(
                  new CreateCommentsViewModel
                  {
                      Content = "1",
                      ProductId = 2,
                      UserId = Guid.NewGuid().ToString(),
                  });
            await service.CreateCommentAsync(
                  new CreateCommentsViewModel
                  {
                      Content = "1",
                      ProductId = 2,
                      UserId = Guid.NewGuid().ToString(),
                  });
            await service.CreateCommentAsync(
                  new CreateCommentsViewModel
                  {
                      Content = "1",
                      ProductId = 2,
                      UserId = Guid.NewGuid().ToString(),
                  });

            Assert.Equal(3, service.GetCommentsCount());
        }

        [Fact]
        public async Task EditCommentTest()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                 .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);
            dbContext.Comments.Add(new Comment
            {
                Id = 1,
                Content = "Unedited",
                UserId = Guid.NewGuid().ToString(),
                ProductId = 3,
            });
            await dbContext.SaveChangesAsync();

            var repository = new EfDeletableEntityRepository<Comment>(dbContext);
            var service = new CommentsService(repository);
            await service.EditCommetAsync(new EditCommentViewModel
            {
                Id = 1,
                Content = "Edited",
                ProductId = 3,
            });
            var comment = service.GetCommentToChange(1);
            Assert.Equal("Edited", comment.Content);
        }

        [Fact]
        public async Task EditCommentWithWrongIdDoenstChangeTest()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                 .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);
            dbContext.Comments.Add(new Comment
            {
                Id = 1,
                Content = "Unedited",
                UserId = Guid.NewGuid().ToString(),
                ProductId = 3,
            });
            await dbContext.SaveChangesAsync();

            var repository = new EfDeletableEntityRepository<Comment>(dbContext);
            var service = new CommentsService(repository);
            await service.EditCommetAsync(new EditCommentViewModel
            {
                Id = 2,
                Content = "Edited",
                ProductId = 3,
            });
            var comment = service.GetCommentToChange(1);
            Assert.Equal("Unedited", comment.Content);
        }

        [Fact]
        public async Task GetCommentToEditTest()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                 .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);
            dbContext.Comments.Add(new Comment
            {
                Id = 1,
                Content = "Test",
                UserId = Guid.NewGuid().ToString(),
                ProductId = 3,
            });
            await dbContext.SaveChangesAsync();

            var repository = new EfDeletableEntityRepository<Comment>(dbContext);
            var service = new CommentsService(repository);
            var comment = service.GetCommentToChange(1);
            Assert.Equal(1, comment.Id);
            Assert.Equal(typeof(EditCommentViewModel), comment.GetType());
        }

        [Theory]
        [InlineData(2)]
        [InlineData(4)]
        [InlineData(10)]
        public async Task GetCommentToEditRetutnsNullIfIdDoentExist(int id)
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                 .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);
            dbContext.Comments.Add(new Comment
            {
                Id = 1,
                Content = "Test",
                UserId = Guid.NewGuid().ToString(),
                ProductId = 3,
            });
            await dbContext.SaveChangesAsync();

            var repository = new EfDeletableEntityRepository<Comment>(dbContext);
            var service = new CommentsService(repository);
            var comment = service.GetCommentToChange(id);
            Assert.Null(comment);
        }
    }
}
