namespace ProductShop.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    using ProductShop.Data.Models;
    using ProductShop.Services.Data;

    [Authorize]
    public class WishesCotroller : Controller
    {
        private readonly Microsoft.AspNetCore.Identity.UserManager<ApplicationUser> manager;
        private readonly IWishesService service;

        public WishesCotroller(UserManager<ApplicationUser> manager, IWishesService service)
        {
            this.manager = manager;
            this.service = service;
        }

        public IActionResult Add(int productId)
        {
            var userId = this.manager.GetUserId(this.User);

            if (this.service.AlredyExists(userId, productId))
            {
                return this.BadRequest();
            }

            return this.RedirectToAction("All", new { userId });
        }

        public IActionResult All()
        {
            var userId = this.manager.GetUserId(this.User);

            var wishedProducts = this.service.All(userId);

            return this.View(wishedProducts);
        }

        public IActionResult Remove(int productId)
        {
            var userId = this.manager.GetUserId(this.User);
            if (!this.service.AlredyExists(userId, productId))
            {
                return this.BadRequest();
            }

            this.service.Remove(userId, productId);

            return this.RedirectToAction("All", new { userId });
        }
    }
}
