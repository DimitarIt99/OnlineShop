namespace ProductShop.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    using ProductShop.Data.Models;

    [Authorize]
    public class OrdersController : Controller
    {
        private readonly UserManager<ApplicationUser> manager;

        public OrdersController(UserManager<ApplicationUser> manager)
        {
            this.manager = manager;
        }

        [HttpPost]
        public async Task<IActionResult> OrderProduct(int productId)
        {
            var userId = this.manager.GetUserId(this.User);



            // validations
            //repository

        }

        public IActionResult MyOrders(int page = 1)
        {
            var userId = this.manager.GetUserId(this.User);

            //repository

            return this.View();
        }
    }
}
