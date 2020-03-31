namespace ProductShop.Web.ViewModels.Subcategories
{
    using ProductShop.Data.Models;
    using ProductShop.Services.Mapping;

    public class SubcategoriesByIdAndName : IMapFrom<Subcategory>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
