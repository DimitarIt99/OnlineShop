namespace ProductShop.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    using ProductShop.Data.Models;
    using ProductShop.Services.Data;
    using ProductShop.Web.ViewModels.Votes;

    [ApiController]
    [Route("api/[controller]")]
    public class VotesController : ControllerBase
    {
        private readonly IVotesService service;
        private readonly UserManager<ApplicationUser> manager;

        public VotesController(IVotesService service, UserManager<ApplicationUser> manager)
        {
            this.service = service;
            this.manager = manager;
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<VotesResponseModel>> Post(VoteInputModel model)
        {
            var userId = this.manager.GetUserId(this.User);
            await this.service.VoteAsync(userId, model.CommentId, model.IsUpvote);
            var votes = this.service.GetVotes(model.CommentId);
            return new VotesResponseModel
            {
                VotesCount = votes,
            };
        }
    }
}
