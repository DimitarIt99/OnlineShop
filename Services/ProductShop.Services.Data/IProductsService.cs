namespace ProductShop.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ProductShop.Web.ViewModels.Products;

    public interface IProductsService
    {
        public Task<int> CreateProduct(CreateProductModel model);

        public DetailsModel ProductDetails(object id);

        public int GetCountByCategoryName(string name);

        public int GetCountByUserId(string userId);

        public IEnumerable<SummaryProductModel> UserProductsById(string userId, int take, int skip = 0);

        public bool ProductQuantityIsPositive(int productId);

        public Task ReduceQuantityByIdAsync(int productId);

        public Task IncreaseQuantityByIdAsync(int productId);
    }
}
