namespace ProductShop.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    using ProductShop.Data.Models;
    using ProductShop.Services.Data;
    using ProductShop.Web.ViewModels.Comments;

    public class CommentsController : Controller
    {
        private readonly ICommentsService service;
        private readonly UserManager<ApplicationUser> userManager;

        public CommentsController(ICommentsService service, UserManager<ApplicationUser> userManager)
        {
            this.service = service;
            this.userManager = userManager;
        }

        [HttpGet]
        public IActionResult Create()
        {
            return this.View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(CreateCommentsViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            model.UserId = this.userManager.GetUserId(this.User);
            await this.service.CreateCommentAsync(model);

            return this.RedirectToAction("Details", "Products", new { id = model.ProductId });
        }
    }
}
