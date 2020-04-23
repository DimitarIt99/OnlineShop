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
        private readonly IDeletableEntityRepository<Product> repository;

        public ProductsService(IDeletableEntityRepository<Product> repository)
        {
            this.repository = repository;
        }

        public async Task<int> CreateProductAsync(CreateProductModel model)
        {
            var idTokens = model.CategoryAndSubcategoryId
                .Split(":", StringSplitOptions.RemoveEmptyEntries)
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

            await this.repository.AddAsync(product);
            await this.repository.SaveChangesAsync();
            return product.Id;
        }

        public DetailsModel ProductDetails(object id)
        {
            var inputId = Convert.ToInt32(id);

            var res = this.repository
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
                    AverageRating = (decimal)p.Ratings.Average(d => (int)d.Grade),
                    UserName = p.User.UserName,
                    PhoneNumber = p.User.PhoneNumber,
                    Comments = p.Comments
                    .Where(c => c.ProductId == inputId)
                    .OrderByDescending(a => a.Votes.Sum(v => (int)v.Type))
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
            => this.repository.All()
            .Where(a => a.Category.Name == name)
            .Where(a => a.Quantity >= 1)
            .Count();

        public IEnumerable<SummaryProductModel> UserProductsById(string userId, int take, int skip = 0)
        => this.repository.All()
            .Where(a => a.UserId == userId)
            .Where(a => a.Quantity >= 1)
            .OrderBy(a => a.CreatedOn)
            .Skip(skip)
            .Take(take)
            .Select(a => new SummaryProductModel
            {
                Id = a.Id,
                ImageUrl = a.ImageUrl,
                Name = a.Name,
                Price = a.Price,
                AverageRating = (decimal)a.Ratings.Average(d => (int)d.Grade),
            })
            .ToList();

        public int GetCountByUserId(string userId)
            => this.repository.All()
            .Where(a => a.User.Id == userId)
            .Where(a => a.Quantity >= 1)
            .Count();

        public bool ProductQuantityIsPositive(int productId)
            => this.repository.All()
            .Where(a => a.Id == productId).Any(a => a.Quantity >= 1);

        public async Task ReduceQuantityByIdAsync(int productId)
        {
            var product = this.repository.All().Where(a => a.Id == productId).FirstOrDefault();

            product.Quantity--;

            await this.repository.SaveChangesAsync();
        }

        public async Task IncreaseQuantityByIdAsync(int productId)
        {
            var product = this.repository.All().Where(a => a.Id == productId).FirstOrDefault();

            product.Quantity++;
            await this.repository.SaveChangesAsync();
        }

        public IEnumerable<SummaryProductModel> ProductsBySubcategoryName(string subcategoryName, int take, int skip = 0)
        {
            return this.repository.All()
                .Where(a => a.Subcategory.Name == subcategoryName)
                .Where(a => a.Quantity >= 1)
                .Skip(skip)
                .Take(take)
                .Select(a => new SummaryProductModel
                {
                    Id = a.Id,
                    AverageRating = (decimal)a.Ratings.Average(a => (int)a.Grade),
                    ImageUrl = a.ImageUrl,
                    Name = a.Name,
                    Price = a.Price,
                })
                .ToList();
        }

        public EditProductViewModel GetProductForEditById(int id)
            => this.repository.All()
            .Where(a => a.Id == id)
            .To<EditProductViewModel>()
            .FirstOrDefault();

        public bool SaleIsByTheUserChanging(string userId, int productId)
            => this.repository.All().Where(a => a.Id == productId)
            .All(a => a.UserId == userId);

        public async Task EditProductAsync(EditProductViewModel model)
        {
            var productToEdit = this.repository
                .All()
                .Where(a => a.Id == model.Id)
                .FirstOrDefault();

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

            productToEdit.ImageUrl = model.ImageUrl;
            productToEdit.Name = model.Name;
            productToEdit.Description = model.Description;
            productToEdit.Price = decimal.Parse(model.Price, CultureInfo.InvariantCulture);
            productToEdit.Quantity = model.Quantity;
            productToEdit.CategoryId = categoryId;
            productToEdit.SubcategoryId = subcategoryId;

            await this.repository.SaveChangesAsync();
        }

        public async Task RemoveProductAsync(int productId)
        {
            var product = this.repository.All().Where(a => a.Id == productId).FirstOrDefault();

            this.repository.Delete(product);

            await this.repository.SaveChangesAsync();
        }
    }
}
