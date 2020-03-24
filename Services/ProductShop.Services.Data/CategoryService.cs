namespace ProductShop.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using ProductShop.Data.Common.Repositories;
    using ProductShop.Data.Models;
    using ProductShop.Services.Mapping;
    using ProductShop.Web.ViewModels.Category;
    using ProductShop.Web.ViewModels.Subcategories;

    public class CategoryService : ICategoryService
    {
        private readonly IDeletableEntityRepository<Category> repository;

        public CategoryService(IDeletableEntityRepository<Category> repository)
        {
            this.repository = repository;
        }

        public IEnumerable<CategoryByNameAndPicture> All()
            => this.repository.All().To<CategoryByNameAndPicture>().ToList();

        public IEnumerable<CategoryNameViewModel> AllCategoryNames()
            => this.repository.All().To<CategoryNameViewModel>().ToList();

        public bool CategoryExist(string name)
            => this.repository.All().Any(c => c.Name == name);

        public int CategoryIdByName(string name)
            => this.repository.All()
            .Where(a => a.Name == name)
            .Select(a => a.Id)
            .FirstOrDefault();

        public NameAndSubcategoriesNamesViewModel SubcateriesNames(string categoryName)
        {
            var categoryId = this.CategoryIdByName(categoryName);

            return this.repository.All()
                .Where(a => a.Id == categoryId)
                .Select(a => new NameAndSubcategoriesNamesViewModel
                {
                    Name = a.Name,
                    Subcategories = a.Subcategories
                    .Where(s => s.CategoryId == categoryId)
                    .Select(s => new SubcategoryNameViewModel
                    {
                        Name = s.Name,
                    })
                    .ToList(),
                })
                .FirstOrDefault();
        }
    }
}
