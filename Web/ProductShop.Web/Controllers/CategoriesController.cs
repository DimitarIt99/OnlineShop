namespace ProductShop.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using ProductShop.Services.Data;
    using ProductShop.Web.ViewModels.Category;

    public class CategoriesController : Controller
    {
        private const int ItemsPerPage = 10;

        private readonly ICategoriesService categoryService;
        private readonly IProductsService productsService;

        public CategoriesController(ICategoriesService categoryService, IProductsService productsService)
        {
            this.categoryService = categoryService;
            this.productsService = productsService;
        }

        public IActionResult All()
        {
            return this.View(this.categoryService.All());
        }

        public IActionResult Details(CategoryNameViewModel model, int page = 1)
        {
            if (!this.categoryService.CategoryExist(model.Name))
            {
                return this.View("ErrorCategory");
            }

            var productsCount = this.productsService.GetCountByCategoryName(model.Name);
            var subcategoryModel = this.categoryService.SubcateriesNames(model.Name, ItemsPerPage, (page - 1) * ItemsPerPage);
            subcategoryModel.PagesCount = ((productsCount - 1) / ItemsPerPage) + 1;
            subcategoryModel.CurrentPage = page;
            return this.View(subcategoryModel);
        }
    }
}
