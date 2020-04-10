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
        private readonly UserManager<ApplicationUser> manager;
        private readonly IOrdersService service;

        public OrdersController(UserManager<ApplicationUser> manager, IOrdersService service)
        {
            this.manager = manager;
            this.service = service;
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

            if (!this.ModelState.IsValid)
            {
                return this.View(model.ProductId);
            }

            if (this.service.AlreadyOrdered(userId, model.ProductId))
            {
                return this.RedirectToAction("AlreadyOrdered", new { productId = model.ProductId });
            }

            await this.service.AddOrderAsync(model);

            return this.RedirectToAction("MyOrders");

        }

        public IActionResult MyOrders(int page = 1)
        {
            var userId = this.manager.GetUserId(this.User);

            //repository

            return this.View();
        }

        public IActionResult OrderState(string id)
        {
            var state = this.service.GetOrderDelivaryState(id);

            return this.View(state);
        }

        public async Task<IActionResult> CancellOrder(RemoveOrderViewModel model)
        {
            if (!this.service.IdExists(model.Id))
            {
                return this.BadRequest();
            }

            await this.service.CancellAsync(model);

            return this.RedirectToAction("MyOrders");
        }

        public IActionResult AlreadyOrdered(int productId)
        {
            return this.View(productId);
        }
    }
}
