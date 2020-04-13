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
        private const int ItemsPerPage = 10;

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

            model.UserId = this.userManager.GetUserId(this.User);
            var id = await this.productsService.CreateProductAsync(model);
            return this.RedirectToAction(nameof(this.Details), new { id });
        }

        [AllowAnonymous]
        public IActionResult Details(int id)
        {
            var fondProduct = this.productsService.ProductDetails(id);
            var userId = this.userManager.GetUserId(this.User);
            fondProduct.IsFavorid = this.wishesService.AlredyExists(userId, id);
            return this.View(fondProduct);
        }

        public IActionResult MyProductsForSale(int page = 1)
        {
            var userId = this.userManager.GetUserId(this.User);

            var productsCount = this.productsService.GetCountByUserId(userId);
            UsersProductsForSaleViewModel usersProducts = new UsersProductsForSaleViewModel()
            {
                CurrentPage = page,
                PagesCount = ((productsCount - 1) / ItemsPerPage) + 1,
            };
            usersProducts.Products = this.productsService.UserProductsById(userId, ItemsPerPage, (page - 1) * ItemsPerPage);
            return this.View(usersProducts);
        }
    }
}
