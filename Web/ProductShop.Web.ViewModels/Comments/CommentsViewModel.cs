namespace ProductShop.Web.ViewModels.Comments
{
    using System;

    using Ganss.XSS;

    public class CommentsViewModel
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        public string Content { get; set; }

        public string SantizedContent => new HtmlSanitizer().Sanitize(this.Content);

        public DateTime CreatedOn { get; set; }

        public int Votes { get; set; }
    }
}
