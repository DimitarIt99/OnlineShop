namespace ProductShop.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ProductShop.Web.ViewModels.Orders;
    using ProductShop.Web.ViewModels.Products;

    public interface IOrdersService
    {
        public Task AddOrderAsync(CreateOrderViewModel model);

        public Task<int> CancelAsync(RemoveOrderViewModel model);

        public IEnumerable<OrderSummaryViewModel> AllOrdersByUserId(string userId, int take, int skip = 0);

        public bool AlreadyOrdered(string userId, int productId);

        public Task ChangeStateAsync(EditStateViewModel model);

        public DelivaryStateViewModel GetOrderDelivaryState(string id);

        public bool IdExists(string id);

        public int OrderedProductsCount(string userId);


    }
}
