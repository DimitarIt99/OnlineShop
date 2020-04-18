namespace ProductShop.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using ProductShop.Services.Data;
    using ProductShop.Web.ViewModels.Products;

    public class ProductsController : AdministrationController
    {
        private readonly IProductsService service;

        public ProductsController(IProductsService service)
        {
            this.service = service;
        }

        public IActionResult Edit(int id)
        {
            // service
            var productToEdit = this.service.GetProductForEditById(id);

            return this.View(productToEdit);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditProductViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            await this.service.EditProductAsync(model);
            return this.RedirectToAction("Details", "Products", new { area = string.Empty, id = model.Id });
        }

        public async Task<IActionResult> Delete(int id)
        {
            await this.service.RemoveProductAsync(id);

            return this.RedirectToAction("Index", "Home", new { area = string.Empty });
        }
    }
}
