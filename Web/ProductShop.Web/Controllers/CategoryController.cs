﻿namespace ProductShop.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using ProductShop.Services.Data;
    using ProductShop.Web.ViewModels.Category;

    public class CategoryController : BaseController
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

        public IActionResult Details(string name)
        {
            return this.View(name);
        }
    }
}
