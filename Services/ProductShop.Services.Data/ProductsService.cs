namespace ProductShop.Services.Data
{
    using System;
    using System.Globalization;
    using System.Linq;
    using System.Threading.Tasks;

    using ProductShop.Data.Common.Repositories;
    using ProductShop.Data.Models;
    using ProductShop.Services.Mapping;
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
                .Select(a => new DetailsModel
                {
                    Id = a.Id,
                    Descrption = a.Description,
                    ImageUrl = a.ImageUrl,
                    Name = a.Name,
                    Price = a.Price,
                    Quantity = a.Quantity,
                    UserUserName = a.User.UserName,
                })
                .FirstOrDefault();
            return res;
        }

        public int GetCountByCategoryName(string name)
            => this.productRepository.All()
            .Where(a => a.Category.Name == name)
            .Count();
    }
}
