namespace ProductShop.Web.ViewModels.Subcategories
{
    using ProductShop.Data.Models;
    using ProductShop.Services.Mapping;

    public class SubcategoryNameViewModel : IMapFrom<Subcategory>
    {
        public string Name { get; set; }
    }
}
