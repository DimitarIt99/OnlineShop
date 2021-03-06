﻿namespace ProductShop.Web.ViewModels.Category
{
    using System.Collections.Generic;

    using ProductShop.Web.ViewModels.Products;
    using ProductShop.Web.ViewModels.Subcategories;

    public class NameAndSubcategoriesNamesViewModel
    {
        public NameAndSubcategoriesNamesViewModel()
        {
            this.Subcategories = new List<SubcategoryNameViewModel>();
            this.Products = new List<SummaryProductModel>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public int PagesCount { get; set; }

        public int CurrentPage { get; set; }

        public IEnumerable<SubcategoryNameViewModel> Subcategories { get; set; }

        public IEnumerable<SummaryProductModel> Products { get; set; }
    }
}
