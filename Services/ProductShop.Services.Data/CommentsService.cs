namespace ProductShop.Services.Data
{
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

        public async Task CreateComment(CreateCommentsViewModel model)
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
    }
}
