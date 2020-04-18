namespace ProductShop.Services.Data
{
    using System.Linq;
    using System.Threading.Tasks;

    using ProductShop.Data.Common.Repositories;
    using ProductShop.Data.Models;
    using ProductShop.Web.ViewModels.Subcategories;

    public class SubcategoriesService : ISubcategoriesService
    {
        private readonly IDeletableEntityRepository<Subcategory> repository;
        private readonly IProductsService productsService;

        public SubcategoriesService(IDeletableEntityRepository<Subcategory> repository, IProductsService productsService)
        {
            this.repository = repository;
            this.productsService = productsService;
        }

        public async Task CreateSubcategoryAsync(CreateSubcategoryViewModel model)
        {
            var subcategory = new Subcategory
            {
                Name = model.Name,
                Description = model.Description,
                CategoryId = model.CategoryId,
            };
            await this.repository.AddAsync(subcategory);
            await this.repository.SaveChangesAsync();
        }

        public async Task DeleteSubcategoryAsync(int id)
        {
            var subcategory = this.GetSubcategoryById(id);
            this.repository.Delete(subcategory);
            await this.repository.SaveChangesAsync();
        }

        public async Task EditSubcategoryAsync(EditSubcategoryViewModel model)
        {
            var subcategory = this.GetSubcategoryById(model.Id);
            subcategory.Name = model.Name;
            subcategory.CategoryId = model.CategoryId;
            subcategory.Description = model.Description;

            await this.repository.SaveChangesAsync();
        }

        public EditSubcategoryViewModel GetSubcategoryForEditById(int id)
        {
            var subcategory = this.GetSubcategoryById(id);

            return new EditSubcategoryViewModel
            {
                CategoryId = subcategory.CategoryId,
                Description = subcategory.Description,
                Id = subcategory.Id,
                Name = subcategory.Name,
            };
        }

        public int ProductsCountBySubcategoryName(string subcategoryName)
            => this.repository.AllAsNoTracking()
            .Where(a => a.Name == subcategoryName)
            .Select(a => a.Products.Where(s => s.Subcategory.Name == subcategoryName))
            .Count();

        public bool SubcategoryExist(string name)
            => this.repository.All().Any(a => a.Name == name);

        private Subcategory GetSubcategoryById(int id)
            => this.repository.All()
            .Where(a => a.Id == id)
            .FirstOrDefault();
    }
}
