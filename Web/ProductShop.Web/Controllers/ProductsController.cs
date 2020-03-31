﻿namespace ProductShop.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using ProductShop.Services.Data;
    using ProductShop.Web.ViewModels.Products;

    [Authorize]
    public class ProductsController : Controller
    {
        private readonly IProductsService productsService;
        private readonly ICategoriesService categoriesService;

        public ProductsController(IProductsService productsService, ICategoriesService categoriesService)
        {
            this.productsService = productsService;
            this.categoriesService = categoriesService;
        }

        public IActionResult AddProduct()
        {
            var categories = this.categoriesService.AllCategoriesAndSubacetoriesByName();
            return this.View(categories);
        }

        [HttpPost]
        public IActionResult AddProduct(CreateProductModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            model.UserId = this.productsService.GetUserId(this.User.Identity.Name);
            this.productsService.CreateProduct(model);

            return this.Redirect("Home/Privacy");
        }
    }
}
