namespace ProductShop.Services.Data
{
    using System.Threading.Tasks;

    public interface IVotesService
    {
        Task VoteAsync(string userId, int commentId, bool isUpvote);

        int GetVotes(int commentId);
    }
}
