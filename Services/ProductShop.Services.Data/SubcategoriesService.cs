namespace ProductShop.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using ProductShop.Data.Common.Repositories;
    using ProductShop.Data.Models;
    using ProductShop.Web.ViewModels.Products;

    public class SubcategoriesService : ISubcategoriesService
    {
        private readonly IDeletableEntityRepository<Subcategory> repository;
        private readonly IProductsService productsService;

        public SubcategoriesService(IDeletableEntityRepository<Subcategory> repository, IProductsService productsService)
        {
            this.repository = repository;
            this.productsService = productsService;
        }

        public int ProductsCountBySubcategoryName(string subcategoryName)
            => this.repository.AllAsNoTracking()
            .Where(a => a.Name == subcategoryName)
            .Select(a => a.Products.Where(s => s.Subcategory.Name == subcategoryName))
            .Count();

        public bool SubcategoryExist(string name)
            => this.repository.All().Any(a => a.Name == name);
    }
}
