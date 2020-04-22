namespace ProductShop.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using ProductShop.Data.Common.Repositories;
    using ProductShop.Data.Models;
    using ProductShop.Services.Mapping;
    using ProductShop.Web.ViewModels.Categories;
    using ProductShop.Web.ViewModels.Category;
    using ProductShop.Web.ViewModels.Products;
    using ProductShop.Web.ViewModels.Subcategories;

    public class CategoriesService : ICategoriesService
    {
        private readonly IDeletableEntityRepository<Category> repository;

        public CategoriesService(IDeletableEntityRepository<Category> repository)
        {
            this.repository = repository;
        }

        public IEnumerable<CategoryByNameAndPicture> AllCategoriesWithTheirePictures()
            => this.repository.All()
            .Select(a => new CategoryByNameAndPicture
            {
                Description = a.Description,
                Id = a.Id,
                ImageUrl = a.ImageUrl,
                Name = a.Name,
                ProductsCount = a.Products
                .Where(s => s.CategoryId == a.Id)
                .Where(s => s.Quantity >= 1)
                .Count(),
            })
            .ToList();

        public IEnumerable<CategoryNameViewModel> AllCategoryNames()
            => this.repository.All()
            .Select(a => new CategoryNameViewModel
            {
                Name = a.Name,
            })
            .ToList();

        public bool CategoryExist(string name)
            => this.repository.All().Any(c => c.Name == name);

        public int CategoryIdByName(string name)
            => this.repository.All()
            .Where(a => a.Name == name)
            .Select(a => a.Id)
            .FirstOrDefault();

        public NameAndSubcategoriesNamesViewModel CategoryDetails(string categoryName, int take = 10, int skip = 0)
        {
            var categoryId = this.CategoryIdByName(categoryName);

            var result = this.repository.All()
                .Where(a => a.Id == categoryId)
                .Select(a => new NameAndSubcategoriesNamesViewModel
                {
                    Id = a.Id,
                    Name = a.Name,
                    Subcategories = a.Subcategories
                    .Where(s => s.CategoryId == categoryId)
                    .Select(s => new SubcategoryNameViewModel
                    {
                        Id = s.Id,
                        Name = s.Name,
                    }),
                    Products = a.Products
                    .Where(s => s.CategoryId == categoryId)
                    .Where(a => a.Quantity >= 1)
                    .Select(s => new SummaryProductModel
                    {
                        Id = s.Id,
                        ImageUrl = s.ImageUrl,
                        Name = s.Name,
                        Price = s.Price,
                        AverageRating = (decimal)s.Ratings.Average(d => (int)d.Grade),
                    })
                    .Skip(skip)
                    .Take(take)
                    .ToList(),
                })
                .FirstOrDefault();
            return result;
        }

        public IEnumerable<CategoriesAndSubcategoriesByNameAndId> AllCategoriesAndSubacetoriesByName()
        {
            return this.repository.All()
                .Select(a => new CategoriesAndSubcategoriesByNameAndId
                {
                    Id = a.Id,
                    Name = a.Name,
                    Subcategories = a.Subcategories
                    .Select(s => new SubcategoriesByIdAndName
                    {
                        Id = s.Id,
                        Name = s.Name,
                    })
                    .ToList(),
                })
                .ToList();
        }

        public async Task CreateCategoryAsync(CreateCategoryViewModel model)
        {
            var category = new Category
            {
                ImageUrl = model.ImageUrl,
                Description = model.Description,
                Name = model.Name,
            };
            await this.repository.AddAsync(category);
            await this.repository.SaveChangesAsync();
        }

        public EditCategoryViewModel FindCategoryById(int id)
        => this.repository.All()
            .Where(a => a.Id == id)
            .Select(a => new EditCategoryViewModel
            {
                Description = a.Description,
                Id = a.Id,
                ImageUrl = a.ImageUrl,
                Name = a.Name,
            })
            .FirstOrDefault();

        public async Task EditCategoryAsync(EditCategoryViewModel model)
        {
            var category = this.GetCategoryById(model.Id);
            category.ImageUrl = model.ImageUrl;
            category.Name = model.Name;
            category.Description = model.Description;

            await this.repository.SaveChangesAsync();
        }

        public async Task DeleteCategoryAsync(int id)
        {
            var category = this.GetCategoryById(id);
            this.repository.Delete(category);
            await this.repository.SaveChangesAsync();
        }

        private Category GetCategoryById(int id)
            => this.repository.All()
            .Where(a => a.Id == id)
            .FirstOrDefault();
    }
}
