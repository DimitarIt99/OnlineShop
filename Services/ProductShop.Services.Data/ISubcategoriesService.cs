namespace ProductShop.Services.Data
{
    using System.Threading.Tasks;

    using ProductShop.Web.ViewModels.Subcategories;

    public interface ISubcategoriesService
    {
        public int ProductsCountBySubcategoryName(string subcategoryName);

        public bool SubcategoryExist(string name);

        public Task CreateSubcategoryAsync(CreateSubcategoryViewModel model);

        public Task EditSubcategoryAsync(EditSubcategoryViewModel model);

        public EditSubcategoryViewModel GetSubcategoryForEditById(int id);

        public Task DeleteSubcategoryAsync(int id);
    }
}
