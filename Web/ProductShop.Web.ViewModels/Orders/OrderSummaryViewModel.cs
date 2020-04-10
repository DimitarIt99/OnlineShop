namespace ProductShop.Web.ViewModels.Orders
{
    public class OrderSummaryViewModel
    {
        public string Id { get; set; }

        public int ProductId { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string ImageUrl { get; set; }
    }
}
