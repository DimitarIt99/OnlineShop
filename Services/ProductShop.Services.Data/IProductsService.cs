namespace ProductShop.Services.Data
{
    using System.Threading.Tasks;

    using ProductShop.Web.ViewModels.Products;

    public interface IProductsService
    {
        public Task<int> CreateProduct(CreateProductModel model);

        public string GetUserId(string userName);

        public DetailsModel ProductDetails(object id);

        public int GetCountByCategoryName(string name);
    }
}
