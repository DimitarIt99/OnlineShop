namespace ProductShop.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    using ProductShop.Data.Models;
    using ProductShop.Services.Data;

    [Authorize]
    public class WishesController : Controller
    {
        private const int ItemsPerPage = 10;

        private readonly UserManager<ApplicationUser> manager;
        private readonly IWishesService service;

        public WishesController(UserManager<ApplicationUser> manager, IWishesService service)
        {
            this.manager = manager;
            this.service = service;
        }

        public async Task<IActionResult> Add(int productId)
        {
            var userId = this.manager.GetUserId(this.User);

            if (this.service.AlredyExists(userId, productId))
            {
                return this.BadRequest();
            }

            await this.service.AddAsync(userId, productId);

            return this.RedirectToAction("All", new { userId });
        }

        public IActionResult All(int page = 1)
        {
            var userId = this.manager.GetUserId(this.User);
            var productsCount = service.GetWishesCountByUserId(userId);
            var wishedProducts = this.service.All(userId, ItemsPerPage, (page - 1) * ItemsPerPage);
            wishedProducts.PagesCount = ((productsCount - 1) / ItemsPerPage) + 1;
            wishedProducts.CurrentPage = page;
            return this.View(wishedProducts);
        }

        public async Task<IActionResult> Remove(int productId)
        {
            var userId = this.manager.GetUserId(this.User);
            if (!this.service.AlredyExists(userId, productId))
            {
                return this.BadRequest();
            }

            await this.service.RemoveAsync(userId, productId);

            return this.RedirectToAction("All", new { userId });
        }
    }
}
