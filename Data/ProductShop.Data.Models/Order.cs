namespace ProductShop.Data.Models
{
    using System;

    using ProductShop.Data.Common.Models;
    using ProductShop.Data.Models.Enums;

    public class Order : BaseModel<string>
    {
        public Order()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public DeliveryState State { get; set; }

        public string Adress { get; set; }

        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public int ProductId { get; set; }

        public Product Product { get; set; }
    }
}
