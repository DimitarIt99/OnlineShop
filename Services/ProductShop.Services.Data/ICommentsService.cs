namespace ProductShop.Services.Data
{
    using System.Threading.Tasks;

    using ProductShop.Web.ViewModels.Comments;

    public interface ICommentsService
    {
        public Task CreateCommentAsync(CreateCommentsViewModel model);

        public EditCommentViewModel GetCommentToChange(int id);

        public Task EditCommetAsync(EditCommentViewModel model);

        public Task DeleteCommentAsync(int id);
    }
}
