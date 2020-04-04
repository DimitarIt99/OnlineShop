namespace ProductShop.Web.ViewComponents
{
    using Microsoft.AspNetCore.Mvc;

    using ProductShop.Services.Data;
    using ProductShop.Web.ViewModels.NavBar;

    public class NavBarViewComponent : ViewComponent
    {
        private readonly ICategoriesService service;

        public NavBarViewComponent(ICategoriesService service)
        {
            this.service = service;
        }

        public IViewComponentResult Invoke()
        {
            var categories = this.service.AllCategoryNames();
            var navBarItems = new NavBarViewModel
            {
                Privacy = "Privacy",
                AddProduct = "Add a Product",
                Category = "Categories",
                Categories = categories,
            };

            return this.View(navBarItems);
        }
    }
}
