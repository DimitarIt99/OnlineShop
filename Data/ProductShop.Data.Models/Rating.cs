namespace ProductShop.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using ProductShop.Data.Common.Models;
    using ProductShop.Data.Models.Enums;

    public class Rating : BaseDeletableModel<int>
    {
        public Grade Grade { get; set; }

        public string Description { get; set; }

        [Required]
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public int ProductId { get; set; }

        public Product Product { get; set; }
    }
}
