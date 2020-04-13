namespace ProductShop.Web.ViewModels.Products
{
    using ProductShop.Data.Models;
    using ProductShop.Services.Mapping;

    public class SummaryProductModel : IMapFrom<Product>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string ImageUrl { get; set; }

        public decimal AverageRating { get; set; }
    }
}
