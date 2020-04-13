namespace ProductShop.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Threading.Tasks;

    using ProductShop.Data.Common.Repositories;
    using ProductShop.Data.Models;
    using ProductShop.Services.Mapping;
    using ProductShop.Web.ViewModels.Comments;
    using ProductShop.Web.ViewModels.Products;

    public class ProductsService : IProductsService
    {
        private readonly IDeletableEntityRepository<Product> productRepository;

        public ProductsService(IDeletableEntityRepository<Product> productRepository)
        {
            this.productRepository = productRepository;
        }

        public async Task<int> CreateProduct(CreateProductModel model)
        {
            var idTokens = model.CategoryAndSubcategoryId
                .Split(":", System.StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();
            var categoryId = idTokens[0];
            int? subcategoryId = null;
            if (idTokens.Length == 2)
            {
                subcategoryId = idTokens[1];
            }

            var price = decimal.Parse(model.Price, CultureInfo.InvariantCulture);
            var product = new Product
            {
                Name = model.Name,
                Price = price,
                Quantity = model.Quantity,
                Description = model.Description,
                ImageUrl = model.ImageUrl,
                CategoryId = categoryId,
                SubcategoryId = subcategoryId,
                UserId = model.UserId,
            };

            await this.productRepository.AddAsync(product);
            await this.productRepository.SaveChangesAsync();
            return product.Id;
        }

        public DetailsModel ProductDetails(object id)
        {
            var inputId = Convert.ToInt32(id);

            var res = this.productRepository
                .All()
                .Where(a => a.Id == inputId)
                .Select(p => new DetailsModel
                {
                    Id = p.Id,
                    Descrption = p.Description,
                    ImageUrl = p.ImageUrl,
                    Name = p.Name,
                    Price = p.Price,
                    Quantity = p.Quantity,
                    UserUserName = p.User.UserName,
                    Comments = p.Comments
                    .Where(c => c.ProductId == inputId)
                    .Select(c => new CommentsViewModel
                    {
                        Id = c.Id,
                        UserName = c.User.UserName,
                        Content = c.Content,
                        CreatedOn = c.CreatedOn,
                        Votes = c.Votes
                        .Sum(v => (int)v.Type),
                    })
                    .ToList(),
                })
                .FirstOrDefault();
            return res;
        }

        public int GetCountByCategoryName(string name)
            => this.productRepository.All()
            .Where(a => a.Category.Name == name)
            .Count();

        public IEnumerable<SummaryProductModel> UserProductsById(string userId, int take, int skip = 0)
        => this.productRepository.All()
            .Where(a => a.UserId == userId)
            .OrderBy(a => a.CreatedOn)
            .Skip(skip)
            .Take(take)
            .Select(a => new SummaryProductModel
            {
                Id = a.Id,
                ImageUrl = a.ImageUrl,
                Name = a.Name,
                Price = a.Price,
            })
            .ToList();

        public int GetCountByUserId(string userId)
            => this.productRepository.All()
            .Where(a => a.User.Id == userId)
            .Count();

        public bool ProductQuantityIsPositive(int productId)
            => this.productRepository.All()
            .Where(a => a.Id == productId).Any(a => a.Quantity >= 1);

        public async Task ReduceQuantityByIdAsync(int productId)
        {
            var product = this.productRepository.All().Where(a => a.Id == productId).FirstOrDefault();

            product.Quantity--;
            await this.productRepository.SaveChangesAsync();
        }

        public async Task IncreaseQuantityByIdAsync(int productId)
        {
            var product = this.productRepository.All().Where(a => a.Id == productId).FirstOrDefault();

            product.Quantity++;
            await this.productRepository.SaveChangesAsync();
        }
    }
}
