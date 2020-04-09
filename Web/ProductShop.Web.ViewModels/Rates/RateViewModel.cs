namespace ProductShop.Web.ViewModels.Rates
{
    using System.ComponentModel.DataAnnotations;

    public class RateViewModel
    {
        [Required]
        [MaxLength(2000)]
        public string Description { get; set; }

        public string UserId { get; set; }

        public int ProductId { get; set; }

        [Range(0, 5)]
        public int Grade { get; set; }
    }
}
