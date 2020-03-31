namespace ProductShop.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using ProductShop.Services.Data;
    using ProductShop.Web.ViewModels.Products;

    [Authorize]
    public class ProductsController : Controller
    {
        private readonly IProductsService service;

        public ProductsController(IProductsService service)
        {
            this.service = service;
        }

        public IActionResult Add()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Add(CreateProductModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            model.UserId = this.service.GetUserId(this.User.Identity.Name);
            this.service.CreateProduct(model);

            return this.Redirect("Home/Privacy");
        }
    }
}
