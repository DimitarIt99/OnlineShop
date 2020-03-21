namespace ProductShop.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using ProductShop.Data.Common.Repositories;
    using ProductShop.Data.Models;
    using ProductShop.Services.Mapping;
    using ProductShop.Web.ViewModels.Category;

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
    }
}
