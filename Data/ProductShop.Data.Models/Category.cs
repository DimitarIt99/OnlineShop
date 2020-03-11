namespace ProductShop.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using ProductShop.Data.Common.Models;

    public class Category : BaseDeletableModel<int>
    {
        public Category()
        {
            this.Products = new HashSet<Product>();
            this.Subcategories = new HashSet<Subcategory>();
        }

        [Required]
        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public string Description { get; set; }

        public ICollection<Product> Products { get; set; }

        public ICollection<Subcategory> Subcategories { get; set; }
    }
}
