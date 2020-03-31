namespace ProductShop.Services.Data
{
    using System.Collections.Generic;

    using ProductShop.Web.ViewModels.Categories;
    using ProductShop.Web.ViewModels.Category;

    public interface ICategoriesService
    {
        IEnumerable<CategoryByNameAndPicture> All();

        IEnumerable<CategoryNameViewModel> AllCategoryNames();

        bool CategoryExist(string name);

        int CategoryIdByName(string name);

        NameAndSubcategoriesNamesViewModel SubcateriesNames(string categoryName);

        public IEnumerable<CategoriesAndSubcategoriesByNameAndId> AllCategoriesAndSubacetoriesByName();
    }
}
