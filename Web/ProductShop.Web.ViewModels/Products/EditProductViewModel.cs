namespace ProductShop.Web.ViewModels.Products
{
    using System.ComponentModel.DataAnnotations;

    using ProductShop.Data.Models;
    using ProductShop.Services.Mapping;

    public class EditProductViewModel : IMapFrom<Product>
    {
        private const string PrideRegex = @"^(?<firstPart>[0-9]+)((?<dot>\.{0,1})((?<=\.)(?<secondPart>[0-9]{1,2}))){0,1}$";

        public int Id { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "Your name can't be larger the 100 symbols.")]
        public string Name { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        [Required]
        [MaxLength(10000, ErrorMessage = "You cant enter a descrption larger then 10000 symbols. Sorry!")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Please enter a price!")]
        [RegularExpression(PrideRegex, ErrorMessage = "The price you entered is either negative or invalid. Please enter a valid price!")]
        public string Price { get; set; }

        [Range(0, int.MaxValue)]
        public int Quantity { get; set; }

        public string UserId { get; set; }

        [AutoMapper.IgnoreMap]
        [Display(Name = "Category")]
        public string CategoryAndSubcategoryId { get; set; }

        public int CategoryId { get; set; }

        public int? SubcategoryId { get; set; }
    }
}
