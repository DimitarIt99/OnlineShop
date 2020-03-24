namespace ProductShop.Web.ViewModels.Category
{
    using System.Collections.Generic;

    using ProductShop.Web.ViewModels.Subcategories;

    public class NameAndSubcategoriesNamesViewModel
    {
        public NameAndSubcategoriesNamesViewModel()
        {
            this.Subcategories = new List<SubcategoryNameViewModel>();
        }

        public string Name { get; set; }

        public IEnumerable<SubcategoryNameViewModel> Subcategories { get; set; }
    }
}
