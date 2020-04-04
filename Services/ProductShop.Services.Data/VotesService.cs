namespace ProductShop.Services.Data
{
    using System.Linq;
    using System.Threading.Tasks;

    using ProductShop.Data.Common.Repositories;
    using ProductShop.Data.Models;
    using ProductShop.Data.Models.Enums;

    public class VotesService : IVotesService
    {
        private readonly IRepository<Vote> repository;

        public VotesService(IRepository<Vote> repository)
        {
            this.repository = repository;
        }

        public int GetVotes(int commentId)
            => this.repository
            .AllAsNoTracking()
            .Where(a => a.CommentId == commentId)
            .Sum(a => (int)a.Type);

        public async Task VoteAsync(string userId, int commentId, bool isUpvote)
        {
            var vote = this.repository
                .All()
                .FirstOrDefault(a => a.UserId == userId && a.CommentId == commentId);

            var type = isUpvote == true ? VoteType.UpVote : VoteType.DownVote;

            if (vote != null)
            {
                vote.Type = type;
            }
            else
            {
                vote = new Vote
                {
                    UserId = userId,
                    CommentId = commentId,
                    Type = type,
                };
                await this.repository.AddAsync(vote);
            }

            await this.repository.SaveChangesAsync();
        }
    }
}
