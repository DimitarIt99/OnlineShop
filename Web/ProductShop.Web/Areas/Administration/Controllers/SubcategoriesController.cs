namespace ProductShop.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    using ProductShop.Services.Data;
    using ProductShop.Web.ViewModels.Subcategories;

    public class SubcategoriesController : AdministrationController
    {
        private readonly ISubcategoriesService service;

        public SubcategoriesController(ISubcategoriesService service)
        {
            this.service = service;
        }

        public IActionResult Create(int categoryId)
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateSubcategoryViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            await this.service.CreateSubcategoryAsync(model);

            return this.RedirectToAction("Index", "Home", new { area = string.Empty });
        }

        public IActionResult Edit(int id)
        {
            var subcategoryViewModel = this.service.GetSubcategoryForEditById(id);

            return this.View(subcategoryViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditSubcategoryViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            await this.service.EditSubcategoryAsync(model);

            return this.RedirectToAction("Index", "Home", new { area = string.Empty });
        }

        public async Task<IActionResult> Delete(int id)
        {
            await this.service.DeleteSubcategoryAsync(id);

            return this.RedirectToAction("Index", "Home", new { area = string.Empty });
        }
    }
}
