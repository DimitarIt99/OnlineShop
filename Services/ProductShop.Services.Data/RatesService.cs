namespace ProductShop.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using ProductShop.Data.Common.Repositories;
    using ProductShop.Data.Models;
    using ProductShop.Data.Models.Enums;
    using ProductShop.Web.ViewModels.Rates;

    public class RatesService : IRatesService
    {
        private readonly IDeletableEntityRepository<Rating> repository;

        public RatesService(IDeletableEntityRepository<Rating> repository)
        {
            this.repository = repository;
        }

        public async Task AddRateAsync(RateViewModel model)
        {
            Rating rating = null;

            if (this.AlreadyRated(model.UserId, model.ProductId))
            {
                rating = this.repository.All()
                    .Where(a => a.UserId == model.UserId && a.ProductId == model.ProductId)
                    .FirstOrDefault();

                rating.Grade = (Grade)model.Grade;
                rating.Description = model.Description;
            }
            else
            {
                rating = new Rating
                {
                    UserId = model.UserId,
                    ProductId = model.ProductId,
                    Description = model.Description,
                    Grade = (Grade)model.Grade,
                };
                await this.repository.AddAsync(rating);
            }

            await this.repository.SaveChangesAsync();
        }

        public bool AlreadyRated(string userId, int productId)
            => this.repository
            .AllAsNoTracking()
            .Any(a => a.ProductId == productId && a.UserId == userId);

        public IEnumerable<ShowRatingsViewModel> RatesLising(int productId, int take, int skip = 0)
            => this.repository.All()
                .Where(a => a.ProductId == productId)
                .Skip(skip)
                .Take(take)
                .Select(a => new ShowRatingsViewModel
                {
                    Username = a.User.UserName,
                    Description = a.Description,
                    Grade = (int)a.Grade,
                })
                .ToList();

        public int RatingsCountByProductId(int productId)
            => this.repository
            .All()
            .Where(a => a.ProductId == productId)
            .Count();
    }
}
