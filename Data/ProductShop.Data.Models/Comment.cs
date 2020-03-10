namespace ProductShop.Data.Models
{
    using ProductShop.Data.Common.Models;

    public class Comment : BaseDeletableModel<int>
    {
        public string Content { get; set; }

        // TO DO maybe do it a diffrent entity
        public int Upvotes { get; set; }

        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public int ProductId { get; set; }

        public Product Product { get; set; }
    }
}
