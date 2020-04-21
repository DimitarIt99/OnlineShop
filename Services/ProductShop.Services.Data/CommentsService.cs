namespace ProductShop.Services.Data
{
    using System.Linq;
    using System.Threading.Tasks;

    using ProductShop.Data.Common.Repositories;
    using ProductShop.Data.Models;
    using ProductShop.Web.ViewModels.Comments;

    public class CommentsService : ICommentsService
    {
        private readonly IDeletableEntityRepository<Comment> repository;

        public CommentsService(IDeletableEntityRepository<Comment> repository)
        {
            this.repository = repository;
        }

        public async Task CreateCommentAsync(CreateCommentsViewModel model)
        {
            var comment = new Comment
            {
                Content = model.Content,
                ProductId = model.ProductId,
                UserId = model.UserId,
            };
            await this.repository.AddAsync(comment);
            await this.repository.SaveChangesAsync();
        }

        public async Task DeleteCommentAsync(int id)
        {
            var comment = this.GetComment(id);

            this.repository.Delete(comment);
            await this.repository.SaveChangesAsync();
        }

        public async Task EditCommetAsync(EditCommentViewModel model)
        {
            var comment = this.GetComment(model.Id);
            if (comment != null)
            {
                comment.Content = model.Content;
            }

            await this.repository.SaveChangesAsync();
        }

        public EditCommentViewModel GetCommentToChange(int id)
        => this.repository.All()
            .Where(a => a.Id == id)
            .Select(a => new EditCommentViewModel
            {
                Content = a.Content,
                Id = a.Id,
                ProductId = a.ProductId,
            })
            .FirstOrDefault();

        public int GetCommentsCount()
            => this.repository.All().Count();

        private Comment GetComment(int id)
            => this.repository.All()
            .Where(a => a.Id == id)
            .FirstOrDefault();
    }
}
