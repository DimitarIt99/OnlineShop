namespace ProductShop.Web.ViewModels.NavBar
{
    using System.Collections.Generic;

    using ProductShop.Web.ViewModels.Category;

    public class NavBarViewModel
    {
        public NavBarViewModel()
        {
            this.Categories = new HashSet<CategoryNameViewModel>();
        }

        public string Privacy { get; set; }

        public string AddProduct { get; set; }

        public string Category { get; set; }

        public IEnumerable<CategoryNameViewModel> Categories { get; set; }
    }
}
