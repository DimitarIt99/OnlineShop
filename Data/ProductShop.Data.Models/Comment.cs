namespace ProductShop.Data.Models
{
    using ProductShop.Data.Common.Models;

    public class Comment : BaseModel<int>
    {
        public string Content { get; set; }

        // TO DO maybe do it a diffrent entity
        public int Rating { get; set; }

        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public int ProductId { get; set; }

        public Product Product { get; set; }
    }
}
