namespace ProductShop.Web.ViewModels.Categories
{
    using System.Collections.Generic;

    using ProductShop.Data.Models;
    using ProductShop.Services.Mapping;
    using ProductShop.Web.ViewModels.Subcategories;

    public class CategoriesAndSubcategoriesByNameAndId : IMapFrom<Category>
    {
        public CategoriesAndSubcategoriesByNameAndId()
        {
            this.Subcategories = new HashSet<SubcategoriesByIdAndName>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<SubcategoriesByIdAndName> Subcategories { get; set; }
    }
}
