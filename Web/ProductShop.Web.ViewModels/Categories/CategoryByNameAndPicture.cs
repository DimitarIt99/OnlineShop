namespace ProductShop.Web.ViewModels.Category
{
    using ProductShop.Data.Models;
    using ProductShop.Services.Mapping;

    public class CategoryByNameAndPicture : IMapFrom<Category>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public string Description { get; set; }

        public int ProductsCount { get; set; }
    }
}
