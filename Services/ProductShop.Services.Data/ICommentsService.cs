namespace ProductShop.Services.Data
{
    using System.Threading.Tasks;

    using ProductShop.Web.ViewModels.Comments;

    public interface ICommentsService
    {
        public Task CreateComment(CreateCommentsViewModel model);
    }
}
