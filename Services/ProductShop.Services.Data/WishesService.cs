namespace ProductShop.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using ProductShop.Data.Common.Repositories;
    using ProductShop.Data.Models;
    using ProductShop.Web.ViewModels.Products;

    public class WishesService : IWishesService
    {
        private readonly IDeletableEntityRepository<Wish> repository;

        public WishesService(IDeletableEntityRepository<Wish> repository)
        {
            this.repository = repository;
        }

        public async Task AddAsync(string userId, int productId)
        {
            var wish = new Wish
            {
                UserId = userId,
                ProductId = productId,
            };

            await this.repository.AddAsync(wish);
            await this.repository.SaveChangesAsync();
        }

        public IEnumerable<SummaryProductModel> All(string userId)
            => this.repository
                .All()
                .Where(a => a.UserId == userId)
                .Select(a => new SummaryProductModel
                {
                    Id = a.Product.Id,
                    Description = a.Product.Description,
                    ImageUrl = a.Product.ImageUrl,
                    Name = a.Product.Name,
                    Price = a.Product.Price,
                })
                .ToList();

        public bool AlredyExists(string userId, int productId)
            => this.repository.All()
            .Any(a => a.UserId == userId && a.ProductId == productId);

        public void Remove(string userId, int productId)
        {
            var wish = this.repository.All().FirstOrDefault(a => a.UserId == userId && a.ProductId == productId);
            this.repository.Delete(wish);
        }
    }
}
