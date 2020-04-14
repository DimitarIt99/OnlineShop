namespace ProductShop.Web.ViewModels.Subcategories
{
    using System.Collections.Generic;

    using ProductShop.Web.ViewModels.Products;

    public class ListingProductsSubcategoryViewModel
    {
        public ListingProductsSubcategoryViewModel()
        {
            this.Products = new HashSet<SummaryProductModel>();
        }

        public string Name { get; set; }

        public int CurrentPage { get; set; }

        public int PagesCount { get; set; }

        public IEnumerable<SummaryProductModel> Products { get; set; }
    }
}
