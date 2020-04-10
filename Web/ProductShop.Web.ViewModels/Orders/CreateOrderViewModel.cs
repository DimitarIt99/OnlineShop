namespace ProductShop.Web.ViewModels.Orders
{
    using System.ComponentModel.DataAnnotations;

    public class CreateOrderViewModel
    {
        public string UserId { get; set; }

        public int ProductId { get; set; }

        [Required]
        public string Adress { get; set; }

        [Range(1, 4)]
        public int State { get; set; }
    }
}
