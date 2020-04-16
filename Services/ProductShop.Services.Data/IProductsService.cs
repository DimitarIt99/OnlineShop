namespace ProductShop.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ProductShop.Web.ViewModels.Products;

    public interface IProductsService
    {
        public Task<int> CreateProductAsync(CreateProductModel model);

        public DetailsModel ProductDetails(object id);

        public int GetCountByCategoryName(string name);

        public int GetCountByUserId(string userId);

        public IEnumerable<SummaryProductModel> UserProductsById(string userId, int take, int skip = 0);

        public bool ProductQuantityIsPositive(int productId);

        public Task ReduceQuantityByIdAsync(int productId);

        public Task IncreaseQuantityByIdAsync(int productId);

        public IEnumerable<SummaryProductModel> ProductsBySubcategoryName(string subcategoryName, int take, int skip = 0);

        public EditProductViewModel GetProductForEditById(int id);

        public bool SaleIsByTheUserChanging(string userId, int productId);

        public Task EditProductAsync(EditProductViewModel model);

        public Task RemoveProductAsync(int productId);
    }
}
