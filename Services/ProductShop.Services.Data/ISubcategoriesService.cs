namespace ProductShop.Services.Data
{
    using System.Collections.Generic;

    using ProductShop.Web.ViewModels.Products;

    public interface ISubcategoriesService
    {
        public int ProductsCountBySubcategoryName(string subcategoryName);

        public bool SubcategoryExist(string name);
    }
}
