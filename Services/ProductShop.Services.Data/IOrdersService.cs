namespace ProductShop.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ProductShop.Web.ViewModels.Orders;
    using ProductShop.Web.ViewModels.Products;

    public interface IOrdersService
    {
        public Task AddOrderAsync(CreateOrderViewModel model);

        public Task CancellAsync(RemoveOrderViewModel model);

        public IEnumerable<SummaryProductModel> AllMyOrders(string userId, int take, int skip = 0);

        public bool AlreadyOrdered(string userId, int productId);

        public Task ChangeState(EditStateViewModel model);

        public bool OrderIdExists(string id);

        public DelivaryStateViewModel GetOrderDelivaryState(string id);
    }
}
