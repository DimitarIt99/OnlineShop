namespace ProductShop.Web.ViewModels.Comments
{
    using System.ComponentModel.DataAnnotations;

    using ProductShop.Data.Models;
    using ProductShop.Services.Mapping;

    public class CreateCommentsViewModel : IMapTo<Comment>
    {
        [Required(ErrorMessage = "The Content are can't be empty. Please enter your comment ther!")]
        [MaxLength(1000, ErrorMessage = "The comment can't be bigger then 1000 symbols.")]
        public string Content { get; set; }

        public int ProductId { get; set; }

        public string UserId { get; set; }
    }
}
