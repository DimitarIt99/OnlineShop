namespace ProductShop.Web.ViewModels.Category
{
    using ProductShop.Data.Models;

    using ProductShop.Services.Mapping;

    public class CategoryNameViewModel : IMapFrom<Category>
    {
        public string Name { get; set; }
    }
}
