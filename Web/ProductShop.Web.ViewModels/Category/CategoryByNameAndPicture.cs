namespace ProductShop.Web.ViewModels.Category
{
    using ProductShop.Data.Models;
    using ProductShop.Services.Mapping;

    public class CategoryByNameAndPicture : IMapFrom<Category>
    {
        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public string Description { get; set; }
    }
}
