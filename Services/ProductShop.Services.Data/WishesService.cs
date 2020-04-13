namespace ProductShop.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using ProductShop.Data.Common.Repositories;
    using ProductShop.Data.Models;
    using ProductShop.Web.ViewModels.Products;
    using ProductShop.Web.ViewModels.Wishes;

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

        public UserWishesViewModel All(string userId, int? take = null, int skip = 0)
        {
            var wishes = this.repository
                  .All()
                  .Where(a => a.UserId == userId)
                  .Where(a => a.Product.Quantity >= 1)
                  .Select(a => new SummaryProductModel
                  {
                      Id = a.Product.Id,
                      ImageUrl = a.Product.ImageUrl,
                      Name = a.Product.Name,
                      Price = a.Product.Price,
                      AverageRating = (decimal)a.Product.Ratings.Average(d => (int)d.Grade),
                  })
                  .Skip(skip)
                  .Take(take.Value)
                  .ToList();

            return this.repository
                  .All()
                  .Where(a => a.UserId == userId)
                  .Select(a => new UserWishesViewModel
                  {
                      Wishes = wishes,
                  })
                  .FirstOrDefault();
        }

        public bool AlredyExists(string userId, int productId)
            => this.repository.All()
            .Any(a => a.UserId == userId && a.ProductId == productId);

        public async Task RemoveAsync(string userId, int productId)
        {
            var wish = this.repository.All()
                .Where(a => a.UserId == userId && a.ProductId == productId)
                .FirstOrDefault();
            this.repository.Delete(wish);
            await this.repository.SaveChangesAsync();
        }

        public int GetWishesCountByUserId(string id)
           => this.repository.All()
           .Where(a => a.UserId == id)
            .Where(a => a.Product.Quantity >= 1)
           .Count();
    }
}
