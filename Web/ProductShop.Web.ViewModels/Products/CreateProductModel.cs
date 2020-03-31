namespace ProductShop.Web.ViewModels.Products
{
    using ProductShop.Data.Models;
    using ProductShop.Services.Mapping;

    public class CreateProductModel : IMapTo<Product>
    {
        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        [AutoMapper.IgnoreMap]
        public string UserId { get; set; }

        public int CategoryId { get; set; }

        public int SubcategoryId { get; set; }
    }
}
