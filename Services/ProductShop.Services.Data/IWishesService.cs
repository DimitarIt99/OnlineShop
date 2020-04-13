namespace ProductShop.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ProductShop.Web.ViewModels.Products;
    using ProductShop.Web.ViewModels.Wishes;

    public interface IWishesService
    {
        public Task AddAsync(string userId, int productId);

        public UserWishesViewModel All(string userId, int? take = null, int skip = 0);

        public Task RemoveAsync(string userId, int productId);

        public bool AlredyExists(string userId, int productId);

        public int GetWishesCountByUserId(string id);
    }
}
