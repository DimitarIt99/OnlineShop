﻿namespace ProductShop.Services.Data
{
    using System.Linq;
    using System.Threading.Tasks;

    using ProductShop.Data.Common.Repositories;
    using ProductShop.Data.Models;
    using ProductShop.Web.ViewModels.Products;

    public class ProductsService : IProductsService
    {
        private readonly IDeletableEntityRepository<Product> productRepository;
        private readonly IDeletableEntityRepository<ApplicationUser> userRepository;

        public ProductsService(IDeletableEntityRepository<Product> productRepository, IDeletableEntityRepository<ApplicationUser> userRepository)
        {
            this.productRepository = productRepository;
            this.userRepository = userRepository;
        }

        public string GetUserId(string userName)
            => this.userRepository.All()
            .Where(a => a.UserName == userName)
            .Select(a => a.Id)
            .FirstOrDefault();

        public async Task<int> CreateProduct(CreateProductModel model)
        {
            var product = new Product
            {
                Name = model.Name,
                Price = model.Price,
                Quantity = model.Quantity,
                Description = model.Description,
                ImageUrl = model.ImageUrl,
                CategoryId = model.CategoryId,
                SubcategoryId = model.SubcategoryId,
                UserId = model.UserId,
            };

            await this.productRepository.AddAsync(product);
            await this.productRepository.SaveChangesAsync();
            return product.Id;
        }
    }
}
