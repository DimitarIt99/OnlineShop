namespace ProductShop.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    using ProductShop.Data.Models;
    using ProductShop.Services.Data;
    using ProductShop.Web.ViewModels.Rates;

    public class RatesController : Controller
    {
        private const int ItemsPerPage = 20;

        private readonly UserManager<ApplicationUser> manager;
        private readonly IRatesService service;

        public RatesController(UserManager<ApplicationUser> manager, IRatesService service)
        {
            this.manager = manager;
            this.service = service;
        }

        [Authorize]
        public IActionResult Rate(int productId)
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Rate(RateViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            string userId = this.manager.GetUserId(this.User);
            model.UserId = userId;
            await this.service.AddRateAsync(model);

            return this.RedirectToAction("AllRating", new { productId = model.ProductId });
        }

        public IActionResult AllRating(int productId, int page = 1)
        {
            var ratingsCount = this.service.RatingsCountByProductId(productId);

            var allRatingsPerPage = new AllShowRatingsViewModel
            {
                CurrentPage = page,
                PagesCount = ((ratingsCount - 1) / ItemsPerPage) + 1,
            };
            allRatingsPerPage.Ratings = this.service.RatesLising(productId, ItemsPerPage, (page - 1) * ItemsPerPage);

            return this.View(allRatingsPerPage);
        }
    }
}
