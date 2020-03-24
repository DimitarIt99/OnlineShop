namespace ProductShop.Services.Data
{
    using System.Collections.Generic;

    using ProductShop.Web.ViewModels.Category;

    public interface ICategoryService
    {
        IEnumerable<CategoryByNameAndPicture> All();

        IEnumerable<CategoryNameViewModel> AllCategoryNames();

        bool CategoryExist(string name);

        int CategoryIdByName(string name);

        NameAndSubcategoriesNamesViewModel SubcateriesNames(string categoryName);
    }
}
