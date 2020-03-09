namespace ProductShop.Data.Models
{
    using ProductShop.Data.Common.Models;

    public class Comment : BaseModel<int>
    {
        public string Content { get; set; }

        // TO DO maybe do it a diffrent entity
        public int Rating { get; set; }
    }
}
