namespace ProductShop.Web.ViewModels.Products
{
    using ProductShop.Data.Models;
    using ProductShop.Services.Mapping;

    public class DetailsModel : IMapFrom<Product>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Descrption { get; set; }

        public string ImageUrl { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public string UserUserName { get; set; }
    }
}
