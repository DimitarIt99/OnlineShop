namespace ProductShop.Web.ViewModels.Orders
{
    using System.Collections.Generic;

    using ProductShop.Web.ViewModels.Products;

    public class ListingOrdersViewModel
    {
        public ListingOrdersViewModel()
        {
            this.Orders = new List<SummaryProductModel>();
        }

        public int CurrentPage { get; set; }

        public int PagesCount { get; set; }

        public IEnumerable<SummaryProductModel> Orders { get; set; }

    }
}
