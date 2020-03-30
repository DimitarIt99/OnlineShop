namespace ProductShop.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using ProductShop.Services.Data;
    using ProductShop.Web.ViewModels.Category;

    public class CategoryController : Controller
    {
        private readonly ICategoryService service;

        public CategoryController(ICategoryService service)
        {
            this.service = service;
        }

        public IActionResult All()
        {
            return this.View(this.service.All());
        }

        public IActionResult Details(CategoryNameViewModel model)
        {
            if (!this.service.CategoryExist(model.Name))
            {
                return this.View("ErrorCategory");
            }

            var subcategoryModel = this.service.SubcateriesNames(model.Name);

            return this.View(subcategoryModel);
        }
    }
}
