namespace ProductShop.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ProductShop.Web.ViewModels.Rates;

    public interface IRatesService
    {
        public Task AddRateAsync(RateViewModel model);

        public bool AlreadyRated(string userId, int productId);

        public IEnumerable<ShowRatingsViewModel> RatesLising(int productId, int take, int skip);

        public int RatingsCountByProductId(int productId);
    }
}
