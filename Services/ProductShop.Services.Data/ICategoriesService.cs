namespace ProductShop.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ProductShop.Web.ViewModels.Categories;
    using ProductShop.Web.ViewModels.Category;

    public interface ICategoriesService
    {
        IEnumerable<CategoryByNameAndPicture> All();

        IEnumerable<CategoryNameViewModel> AllCategoryNames();

        bool CategoryExist(string name);

        int CategoryIdByName(string name);

        NameAndSubcategoriesNamesViewModel SubcateriesNames(string categoryName, int? take = null, int skip = 0);

        public IEnumerable<CategoriesAndSubcategoriesByNameAndId> AllCategoriesAndSubacetoriesByName();

        public Task CreateCategoryAsync(CreateCategoryViewModel model);

        public EditCategoryViewModel FindCategoryById(int id);

        public Task EditCategoryAsync(EditCategoryViewModel model);

        public Task DeleteCategoryViewModelAsync(int id);
    }
}
