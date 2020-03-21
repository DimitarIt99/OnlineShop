namespace ProductShop.Services.Data
{
    using System.Collections.Generic;

    using ProductShop.Web.ViewModels.Category;

    public interface ICategoryService
    {
        IEnumerable<CategoryByNameAndPicture> All();

        IEnumerable<CategoryNameViewModel> AllCategoryNames();
    }
}
