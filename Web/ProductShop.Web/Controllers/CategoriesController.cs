namespace ProductShop.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using ProductShop.Services.Data;
    using ProductShop.Web.ViewModels.Category;

    public class CategoriesController : Controller
    {
        private const int ItemsPerPage = 10;

        private readonly ICategoriesService service;
        private readonly IProductsService productsService;

        public CategoriesController(ICategoriesService categoryService, IProductsService productsService)
        {
            this.service = categoryService;
            this.productsService = productsService;
        }

        public IActionResult All()
        {
            return this.View(this.service.All());
        }

        public IActionResult Details(CategoryNameViewModel model, int page = 1)
        {
            if (!this.service.CategoryExist(model.Name))
            {
                return this.View("ErrorCategory");
            }

            var productsCount = this.productsService.GetCountByCategoryName(model.Name);
            var subcategoryModel = this.service.SubcateriesNames(model.Name, ItemsPerPage, (int)(page - 1) * ItemsPerPage);
            subcategoryModel.PagesCount = ((productsCount - 1) / ItemsPerPage) + 1;
            return this.View(subcategoryModel);
        }
    }
}
