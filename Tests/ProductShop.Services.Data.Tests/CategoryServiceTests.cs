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
    using ProductShop.Web.ViewModels.Categories;
    using ProductShop.Web.ViewModels.Category;
    using ProductShop.Web.ViewModels.Comments;
    using ProductShop.Web.ViewModels.Products;
    using ProductShop.Web.ViewModels.Subcategories;
    using Xunit;

    public class CategoryServiceTests
    {
        [Fact]
        public async Task AllCategoriesWithPicturesTest()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);
            dbContext.Categories.Add(new Category
            {
                Id = 1,
                Description = "Test",
                Name = "Test1",
                ImageUrl = "Picture1",
                Products = new List<Product>
                {
                    new Product
                    {
                        CategoryId = 1,
                        Quantity = 3,
                    },
                    new Product
                    {
                        CategoryId = 1,
                        Quantity = 3,
                    },
                    new Product
                    {
                        CategoryId = 1,
                        Quantity = 3,
                    },
                    new Product
                    {
                        CategoryId = 1,
                        Quantity = 3,
                    },
                },
            });
            dbContext.Categories.Add(new Category
            {
                Id = 2,
                Description = "Test2",
                Name = "Test2",
                ImageUrl = "Picture2",
                Products = new List<Product>
                {
                    new Product
                    {
                        CategoryId = 2,
                        Quantity = 1,
                    },
                    new Product
                    {
                        CategoryId = 2,
                        Quantity = 1,
                    },
                    new Product
                    {
                        CategoryId = 2,
                        Quantity = 1,
                    },
                },
            });
            await dbContext.SaveChangesAsync();

            var repository = new EfDeletableEntityRepository<Category>(dbContext);
            var service = new CategoriesService(repository);
            var categories = service.AllCategoriesWithTheirePictures();
            Assert.NotNull(categories);
            Assert.Equal(typeof(CategoryByNameAndPicture), categories.First().GetType());
            Assert.Equal("Test1", categories.Where(a => a.Id == 1).First().Name);
            Assert.Equal(4, categories.Where(a => a.Id == 1).First().ProductsCount);
            Assert.Equal(3, categories.Where(a => a.Id == 2).First().ProductsCount);
        }

        [Fact]
        public async Task ProductsWithLessThenOneQuantityDontCountTest()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);
            dbContext.Categories.Add(new Category
            {
                Id = 1,
                Description = "Test",
                Name = "Test1",
                ImageUrl = "Picture1",
                Products = new List<Product>
                {
                    new Product
                    {
                        CategoryId = 1,
                        Quantity = 3,
                    },
                    new Product
                    {
                        CategoryId = 1,
                        Quantity = 3,
                    },
                    new Product
                    {
                        CategoryId = 1,
                        Quantity = 0,
                    },
                    new Product
                    {
                        CategoryId = 1,
                        Quantity = 0,
                    },
                },
            });
            await dbContext.SaveChangesAsync();

            var repository = new EfDeletableEntityRepository<Category>(dbContext);
            var service = new CategoriesService(repository);
            var categories = service.AllCategoriesWithTheirePictures();
            Assert.Equal(2, categories.Where(a => a.Id == 1).First().ProductsCount);
        }

        [Fact]
        public async Task AllCategoryNamesTest()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);
            dbContext.Categories.Add(new Category
            {
                Id = 1,
                Name = "Test1",
            });
            await dbContext.SaveChangesAsync();
            var repository = new EfDeletableEntityRepository<Category>(dbContext);
            var service = new CategoriesService(repository);
            var categoryNames = service.AllCategoryNames();

            Assert.Single(categoryNames);
            Assert.Equal("Test1", categoryNames.First().Name);
        }

        [Fact]
        public async Task CategoryExistTest()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);
            dbContext.Categories.Add(new Category
            {
                Name = "Test1",
            });
            await dbContext.SaveChangesAsync();
            var repository = new EfDeletableEntityRepository<Category>(dbContext);
            var service = new CategoriesService(repository);
            Assert.True(service.CategoryExist("Test1"));
        }

        [Fact]
        public async Task CategoryDoesntExistTest()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);
            dbContext.Categories.Add(new Category
            {
                Name = "Fist",
            });
            await dbContext.SaveChangesAsync();
            var repository = new EfDeletableEntityRepository<Category>(dbContext);
            var service = new CategoriesService(repository);
            Assert.False(service.CategoryExist("Test1"));
        }

        [Fact]
        public async Task CategoryIdByNameTest()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);
            dbContext.Categories.Add(new Category
            {
                Id = 1,
                Name = "First",
            });
            dbContext.Categories.Add(new Category
            {
                Id = 2,
                Name = "Second",
            });
            await dbContext.SaveChangesAsync();
            var repository = new EfDeletableEntityRepository<Category>(dbContext);
            var service = new CategoriesService(repository);
            Assert.Equal(1, service.CategoryIdByName("First"));
            Assert.Equal(2, service.CategoryIdByName("Second"));
        }

        [Fact]
        public async Task CategoryIdByNameReturnsZeroTest()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);
            dbContext.Categories.Add(new Category
            {
                Id = 1,
                Name = "First",
            });
            dbContext.Categories.Add(new Category
            {
                Id = 2,
                Name = "Second",
            });
            await dbContext.SaveChangesAsync();
            var repository = new EfDeletableEntityRepository<Category>(dbContext);
            var service = new CategoriesService(repository);
            Assert.Equal(0, service.CategoryIdByName("Doenst exist"));
        }

        [Fact]
        public async Task CategoryDetailsTest()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);
            dbContext.Categories.Add(new Category
            {
                Id = 1,
                Name = "First",
                Products = new List<Product>
                {
                    new Product
                    {
                        Id = 1,
                        ImageUrl = "Test1",
                        Name = "Test1",
                        Price = 5,
                        Quantity = 3,
                        CategoryId = 1,
                        Ratings = new List<Rating>
                        {
                            new Rating
                            {
                                Grade = Grade.VeryGood,
                            },
                        },
                    },
                    new Product
                    {
                        Id = 2,
                        ImageUrl = "Test2",
                        Name = "Test2",
                        Price = 5,
                        Quantity = 3,
                        CategoryId = 1,
                        Ratings = new List<Rating>
                        {
                            new Rating
                            {
                                Grade = Grade.VeryGood,
                            },
                            new Rating
                            {
                                Grade = Grade.VeryBad,
                            },
                        },
                    },
                    new Product
                    {
                        Id = 3,
                        ImageUrl = "Test3",
                        Name = "Test3",
                        Price = 5,
                        Quantity = 3,
                        CategoryId = 1,
                        Ratings = new List<Rating>
                        {
                            new Rating
                            {
                                Grade = Grade.VeryGood,
                            },
                        },
                    },
                    new Product
                   {
                       Id = 4,
                       ImageUrl = "Test3",
                       Name = "Test3",
                       Price = 5,
                       Quantity = 0,
                       CategoryId = 1,
                       Ratings = new List<Rating>
                       {
                           new Rating
                           {
                               Grade = Grade.VeryGood,
                           },
                       },
                   },
                },
                Subcategories = new List<Subcategory>
                {
                new Subcategory
                {
                      Id = 1,
                      Name = "Test1",
                      CategoryId = 1,
                },
                new Subcategory
                {
                      Id = 2,
                      Name = "Test2",
                      CategoryId = 1,
                },
                new Subcategory
                {
                      Id = 3,
                      Name = "Test3",
                      CategoryId = 1,
                },
                },
            });
            await dbContext.SaveChangesAsync();
            var repository = new EfDeletableEntityRepository<Category>(dbContext);
            var service = new CategoriesService(repository);
            var detailsModel = service.CategoryDetails("First", 3, 1);
            Assert.Equal(typeof(NameAndSubcategoriesNamesViewModel), detailsModel.GetType());
            Assert.Equal(typeof(SubcategoryNameViewModel), detailsModel.Subcategories.First().GetType());
            Assert.Equal(typeof(SummaryProductModel), detailsModel.Products.FirstOrDefault().GetType());
            Assert.Equal(3, detailsModel.Products.Where(a => a.Id == 2).First().AverageRating);
            Assert.Equal(2, detailsModel.Products.Count());
            Assert.Contains(detailsModel.Subcategories, a => a.Id == 1 && a.Name == "Test1");
            Assert.Equal("First", detailsModel.Name);
        }

        [Fact]
        public async Task CreateCategoryTest()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);

            var repository = new EfDeletableEntityRepository<Category>(dbContext);
            var service = new CategoriesService(repository);

            var category = new CreateCategoryViewModel
            {
                Description = "Test",
                ImageUrl = "Test",
                Name = "Test",
            };

            await service.CreateCategoryAsync(category);

            Assert.True(service.CategoryExist("Test"));
        }

        [Fact]
        public async Task FindCategoryByIdTest()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);
            dbContext.Categories.Add(
                new Category
                {
                    Id = 1,
                    Name = "Test1",
                });
            dbContext.Categories.Add(
                new Category
                {
                    Id = 2,
                    Name = "Test2",
                });
            await dbContext.SaveChangesAsync();
            var repository = new EfDeletableEntityRepository<Category>(dbContext);
            var service = new CategoriesService(repository);

            var category = service.FindCategoryById(1);

            Assert.Equal(typeof(EditCategoryViewModel), category.GetType());
            Assert.Equal("Test1", category.Name);
        }

        [Fact]
        public async Task EditCategoryAsyncTest()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);
            dbContext.Categories.Add(
                new Category
                {
                    Id = 1,
                    Name = "Test1",
                });
            dbContext.Categories.Add(
                new Category
                {
                    Id = 2,
                    Name = "Test2",
                });
            await dbContext.SaveChangesAsync();
            var repository = new EfDeletableEntityRepository<Category>(dbContext);
            var service = new CategoriesService(repository);

            var category = service.FindCategoryById(2);
            category.Name = "Edited";

            await service.EditCategoryAsync(category);
            var editedCategory = service.FindCategoryById(2);
            Assert.Equal(typeof(EditCategoryViewModel), category.GetType());
            Assert.Equal(category.Name, editedCategory.Name);
        }

        [Fact]
        public async Task DeleteCategoryAsyncTest()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var dbContext = new ApplicationDbContext(options);
            dbContext.Categories.Add(
                new Category
                {
                    Id = 1,
                    Name = "Test1",
                });
            dbContext.Categories.Add(
                new Category
                {
                    Id = 2,
                    Name = "Test2",
                });
            await dbContext.SaveChangesAsync();
            var repository = new EfDeletableEntityRepository<Category>(dbContext);
            var service = new CategoriesService(repository);
            await service.DeleteCategoryAsync(1);

            Assert.Single(service.AllCategoryNames());
        }
    }
}
