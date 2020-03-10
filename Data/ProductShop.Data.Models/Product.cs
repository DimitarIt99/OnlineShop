namespace ProductShop.Data.Models
{
    using System.Collections.Generic;

    using ProductShop.Data.Common.Models;

    public class Product : BaseDeletableModel<int>
    {
        public Product()
        {
            this.Comments = new HashSet<Comment>();
            this.Orders = new HashSet<Order>();
            this.Ratings = new HashSet<Rating>();
        }

        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public string ApplicationUserId { get; set; }

        public ApplicationUser ApplicationUser { get; set; }

        public ICollection<Comment> Comments { get; set; }

        public int CategoryId { get; set; }

        public Category Category { get; set; }

        public int SubcategoryId { get; set; }

        public Subcategory Subcategory { get; set; }

        public ICollection<Order> Orders { get; set; }

        public ICollection<Rating> Ratings { get; set; }
    }
}
