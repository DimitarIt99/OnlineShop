namespace ProductShop.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using ProductShop.Data.Models;
    using ProductShop.Services.Data;
    using ProductShop.Web.ViewModels.Products;

    [Authorize]
    public class ProductsController : Controller
    {
        private readonly IProductsService productsService;
        private readonly ICategoriesService categoriesService;
        private readonly IWishesService wishesService;
        private readonly UserManager<ApplicationUser> userManager;

        public ProductsController(
            IProductsService productsService,
            ICategoriesService categoriesService,
            IWishesService wishesService,
            UserManager<ApplicationUser> userManager)
        {
            this.productsService = productsService;
            this.categoriesService = categoriesService;
            this.wishesService = wishesService;
            this.userManager = userManager;
        }

        public IActionResult AddProduct()
        {
            var categories = this.categoriesService.AllCategoriesAndSubacetoriesByName();
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(CreateProductModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            // model.UserId = this.productsService.GetUserId(this.User.Identity.Name);
            var id = await this.productsService.CreateProduct(model);
            return this.RedirectToAction(nameof(this.Details), new { id });
        }

        public IActionResult Details(int id)
        {
            var fondProduct = this.productsService.ProductDetails(id);
            var userId = this.userManager.GetUserId(this.User);
            fondProduct.IsFavorid = this.wishesService.AlredyExists(userId, id);
            return this.View(fondProduct);
        }
    }
}
