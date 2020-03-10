namespace ProductShop.Data.Models
{
    using ProductShop.Data.Common.Models;
    using ProductShop.Data.Models.Enums;

    public class Rating : BaseDeletableModel<int>
    {
        public Grade Grade { get; set; }

        public string ApplicationUserId { get; set; }

        public ApplicationUser User { get; set; }

        public int ProductId { get; set; }

        public Product Product { get; set; }
    }
}
