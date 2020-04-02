namespace ProductShop.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using ProductShop.Data.Common.Models;

    public class Product : BaseDeletableModel<int>
    {
        public Product()
        {
            this.Comments = new HashSet<Comment>();
            this.Orders = new HashSet<Order>();
            this.Ratings = new HashSet<Rating>();
        }

        [Required]
        public string Name { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }

        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public ICollection<Comment> Comments { get; set; }

        public int CategoryId { get; set; }

        public Category Category { get; set; }

        public int? SubcategoryId { get; set; }

        public Subcategory Subcategory { get; set; }

        public ICollection<Order> Orders { get; set; }

        public ICollection<Rating> Ratings { get; set; }
    }
}
