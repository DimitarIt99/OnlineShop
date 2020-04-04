namespace ProductShop.Data.Models
{
    using ProductShop.Data.Common.Models;
    using ProductShop.Data.Models.Enums;

    public class Vote : BaseModel<int>
    {
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public int CommentId { get; set; }

        public Comment Comment { get; set; }

        public VoteType Type { get; set; }
    }
}
