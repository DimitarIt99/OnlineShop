namespace ProductShop.Web.ViewModels.Category
{
    using System.Collections.Generic;

    using ProductShop.Web.ViewModels.Products;
    using ProductShop.Web.ViewModels.Subcategories;

    public class NameAndSubcategoriesNamesViewModel
    {
        public NameAndSubcategoriesNamesViewModel()
        {
            this.Subcategories = new List<SubcategoryNameViewModel>();
            this.Products = new List<SummaryProductModel>();
        }

        public string Name { get; set; }

        public IEnumerable<SubcategoryNameViewModel> Subcategories { get; set; }

        public IEnumerable<SummaryProductModel> Products { get; set; }
    }
}
