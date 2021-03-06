﻿namespace ProductShop.Web.ViewModels.Products
{
    using System.Collections.Generic;
    using Ganss.XSS;
    using ProductShop.Data.Models;
    using ProductShop.Services.Mapping;
    using ProductShop.Web.ViewModels.Comments;

    public class DetailsModel : IMapFrom<Product>
    {
        public DetailsModel()
        {
            this.Comments = new HashSet<CommentsViewModel>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Descrption { get; set; }

        public string SanitaziedDescription => new HtmlSanitizer().Sanitize(this.Descrption);

        public string ImageUrl { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public string UserName { get; set; }

        public string PhoneNumber { get; set; }

        public decimal AverageRating { get; set; }

        public int VotesCount { get; set; }

        public bool IsFavorid { get; set; }

        public ICollection<CommentsViewModel> Comments { get; set; }
    }
}
