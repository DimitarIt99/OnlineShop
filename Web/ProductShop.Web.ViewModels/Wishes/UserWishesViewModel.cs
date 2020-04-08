namespace ProductShop.Web.ViewModels.Wishes
{
    using System.Collections.Generic;

    using ProductShop.Web.ViewModels.Products;

    public class UserWishesViewModel
    {
        public UserWishesViewModel()
        {
            this.Wishes = new List<SummaryProductModel>();
        }

        public int CurrentPage { get; set; }

        public int PagesCount { get; set; }

        public IEnumerable<SummaryProductModel> Wishes { get; set; }
    }
}
