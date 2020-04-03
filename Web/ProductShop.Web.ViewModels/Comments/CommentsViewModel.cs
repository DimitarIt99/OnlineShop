namespace ProductShop.Web.ViewModels.Comments
{
    using System;

    public class CommentsViewModel
    {
        public string UserName { get; set; }

        public string Content { get; set; }

        public DateTime CreatedOn { get; set; }

        public int Votes { get; set; }
    }
}
