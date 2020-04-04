namespace ProductShop.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ProductShop.Web.ViewModels.Products;

    public interface IWishesService
    {
        public Task AddAsync(string userId, int productId);

        public IEnumerable<SummaryProductModel> All(string userId);

        public void Remove(string userId, int productId);

        public bool AlredyExists(string userId, int productId);
    }
}
