namespace ProductShop.Data.Models
{
    using ProductShop.Data.Common.Models;

    public class Product : BaseModel<int>
    {
        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }
    }
}
