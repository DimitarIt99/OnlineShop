namespace ProductShop.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    using ProductShop.Data.Models;
    using ProductShop.Services.Data;
    using ProductShop.Web.ViewModels.Orders;

    [Authorize]
    public class OrdersController : Controller
    {
        private const int ItemsPerPage = 10;

        private readonly UserManager<ApplicationUser> manager;
        private readonly IOrdersService ordersService;
        private readonly IProductsService productsService;

        public OrdersController(UserManager<ApplicationUser> manager, IOrdersService ordersService, IProductsService productsService)
        {
            this.manager = manager;
            this.ordersService = ordersService;
            this.productsService = productsService;
        }

        [HttpGet]
        public IActionResult OrderProduct(int productId)
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> OrderProduct(CreateOrderViewModel model)
        {
            var userId = this.manager.GetUserId(this.User);

            model.UserId = userId;
            if (!this.productsService.ProductQuantityIsPositive(model.ProductId))
            {
                return this.BadRequest();
            }

            await this.ordersService.AddOrderAsync(model);
            await this.productsService.ReduceQuantityByIdAsync(model.ProductId);
            return this.RedirectToAction("MyOrders");
        }

        public IActionResult MyOrders(int page = 1)
        {
            var userId = this.manager.GetUserId(this.User);
            var ordersCount = this.ordersService.OrderedProductsCount(userId);
            var orders = new ListingOrdersViewModel
            {
                CurrentPage = page,
                PagesCount = ((ordersCount - 1) / ItemsPerPage) + 1,
            };
            orders.Orders = this.ordersService.AllOrdersByUserId(userId, ItemsPerPage, (page - 1) * ItemsPerPage);

            return this.View(orders);
        }

        public IActionResult OrderState(string id)
        {
            var state = this.ordersService.GetOrderDelivaryState(id);

            return this.View(state);
        }

        [HttpGet]
        public IActionResult CancellOrder(RemoveOrderViewModel model)
        {
            return this.View(model);
        }

        public async Task<IActionResult> CancellOrderDb(string id)
        {
            RemoveOrderViewModel model = new RemoveOrderViewModel
            {
                Id = id,
            };

            var productId = await this.ordersService.CancelAsync(model);
            await this.productsService.IncreaseQuantityByIdAsync(productId);
            return this.RedirectToAction("MyOrders");
        }

        public IActionResult AlreadyOrdered(int productId)
        {
            return this.View(productId);
        }
    }
}
