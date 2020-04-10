namespace ProductShop.Web.ViewModels.Orders
{
    using System.Collections.Generic;

    using ProductShop.Web.ViewModels.Products;

    public class ListingOrdersViewModel
    {
        public ListingOrdersViewModel()
        {
            this.Orders = new List<OrderSummaryViewModel>();
        }

        public int CurrentPage { get; set; }

        public int PagesCount { get; set; }

        public IEnumerable<OrderSummaryViewModel> Orders { get; set; }

    }
}
