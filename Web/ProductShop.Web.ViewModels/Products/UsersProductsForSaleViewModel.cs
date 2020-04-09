namespace ProductShop.Web.ViewModels.Products
{
    using System.Collections.Generic;

    public class UsersProductsForSaleViewModel
    {
        public UsersProductsForSaleViewModel()
        {
            this.Products = new HashSet<SummaryProductModel>();
        }

        public int CurrentPage { get; set; }

        public int PagesCount { get; set; }

        public IEnumerable<SummaryProductModel> Products { get; set; }
    }
}
