namespace ProductShop.Web.ViewModels.Rates
{
    using System.ComponentModel.DataAnnotations;

    public class ShowRatingsViewModel
    {
        public string Username { get; set; }

        public string Description { get; set; }

        public int Grade { get; set; }
    }
}
