namespace ProductShop.Data.Models
{
    using System.Collections.Generic;

    using ProductShop.Data.Common.Models;

    public class Subcategory : BaseDeletableModel<int>
    {
        public Subcategory()
        {
            this.Products = new HashSet<Product>();
        }

        public string Name { get; set; }

        public string Description { get; set; }

        public int CategoryId { get; set; }

        public Category Category { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}
