﻿namespace ProductShop.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using ProductShop.Services.Data;
    using ProductShop.Web.ViewModels.Comments;

    public class CommentsController : AdministrationController
    {
        private readonly ICommentsService service;

        public CommentsController(ICommentsService service)
        {
            this.service = service;
        }

        public IActionResult Edit(int id)
        {
            var commentViewModel = this.service.GetCommentToChange(id);
            return this.View(commentViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditCommentViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            await this.service.EditCommetAsync(model);

            return this.RedirectToAction("Index", "Home", new { area = string.Empty});
        }

        public async Task<IActionResult> Delete(int id)
        {
            await this.service.DeleteCommentAsync(id);

            return this.RedirectToAction("Index", "Home", new { area = string.Empty });
        }
    }
}
