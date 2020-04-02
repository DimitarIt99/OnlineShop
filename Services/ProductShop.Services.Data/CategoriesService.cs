﻿namespace ProductShop.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;

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
                    Products = a.Products
                    .Where(s => s.CategoryId == categoryId)
                    .Select(s => new SummaryProductModel
                    {
                        Id = s.Id,
                        Description = s.Description,
                        ImageUrl = s.ImageUrl,
                        Name = s.Name,
                        Price = s.Price,
                    })
                    .ToList(),
                })
                .FirstOrDefault();
        }

        public IEnumerable<CategoriesAndSubcategoriesByNameAndId> AllCategoriesAndSubacetoriesByName()
        {
            return this.repository.All()
                .To<CategoriesAndSubcategoriesByNameAndId>()
                .ToList();
        }
    }
}