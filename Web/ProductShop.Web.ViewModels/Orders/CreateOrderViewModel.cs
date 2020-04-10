namespace ProductShop.Web.ViewModels.Orders
{
    public class CreateOrderViewModel
    {
        public string UserId { get; set; }

        public int ProductId { get; set; }

        public string Adress { get; set; }

        public int State { get; set; }
    }
}
