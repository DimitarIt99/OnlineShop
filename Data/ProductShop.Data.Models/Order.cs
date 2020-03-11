namespace ProductShop.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using ProductShop.Data.Common.Models;
    using ProductShop.Data.Models.Enums;

    public class Order : BaseDeletableModel<string>
    {
        public Order()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public DeliveryState State { get; set; }

        [Required]
        public string Adress { get; set; }

        [Required]
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public int ProductId { get; set; }

        public Product Product { get; set; }
    }
}
