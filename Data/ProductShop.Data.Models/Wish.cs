namespace ProductShop.Data.Models
{
    using System;

    using ProductShop.Data.Common.Models;

    public class Wish : BaseDeletableModel<string>
    {
        public Wish()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public int ProductId { get; set; }

        public Product Product { get; set; }

        public string UserId { get; set; }

        public ApplicationUser User { get; set; }
    }
}
