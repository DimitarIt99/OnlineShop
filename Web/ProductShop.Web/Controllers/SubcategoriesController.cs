namespace ProductShop.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using ProductShop.Services.Data;
    using ProductShop.Web.ViewModels.Subcategories;

    public class SubcategoriesController : Controller
    {
        private const int ItemsPerPage = 10;
        private readonly IProductsService productsService;
        private readonly ISubcategoriesService subcategoriesService;

        public SubcategoriesController(IProductsService productsService, ISubcategoriesService subcategoriesService)
        {
            this.productsService = productsService;
            this.subcategoriesService = subcategoriesService;
        }

        public IActionResult Details(ListingProductsSubcategoryViewModel model, int page = 1)
        {
            var productsCount = this.subcategoriesService.ProductsCountBySubcategoryName(model.Name);

            var subcategoryModel = new ListingProductsSubcategoryViewModel
            {
                Name = model.Name,
                CurrentPage = page,
                PagesCount = ((productsCount - 1) / ItemsPerPage) + 1,
            };

            subcategoryModel.Products = this.productsService.ProductsBySubcategoryName(model.Name, ItemsPerPage, (page - 1) * ItemsPerPage);
            return this.View(subcategoryModel);
        }
    }
}
