namespace ProductShop.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    using ProductShop.Services.Data;
    using ProductShop.Web.ViewModels.Categories;

    public class CategoriesController : AdministrationController
    {
        private readonly ICategoriesService service;

        public CategoriesController(ICategoriesService service)
        {
            this.service = service;
        }

        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCategoryViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            await this.service.CreateCategoryAsync(model);
            return this.RedirectToAction("All", "Categories", new { area = string.Empty });
        }

        public IActionResult Edit(int id)
        {
            var categoryViewModel = this.service.FindCategoryById(id);

            return this.View(categoryViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditCategoryViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            await this.service.EditCategoryAsync(model);

            return this.RedirectToAction("All", "Categories", new { area = string.Empty });
        }

        public async Task<IActionResult> Delete(int id)
        {
            await this.service.DeleteCategoryAsync(id);
            return this.RedirectToAction("All", "Categories", new { area = string.Empty });
        }
    }
}
