namespace ProductShop.Web.ViewModels.Rates
{
    using System.Collections.Generic;

    public class AllShowRatingsViewModel
    {
        public int CurrentPage { get; set; }

        public int PagesCount { get; set; }

        public IEnumerable<ShowRatingsViewModel> Ratings { get; set; }
    }
}
