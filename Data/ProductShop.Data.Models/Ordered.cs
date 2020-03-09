namespace ProductShop.Data.Models
{
    using ProductShop.Data.Common.Models;
    using ProductShop.Data.Models.Enums;

    public class Ordered : BaseModel<string>
    {
        public bool InDelivery { get; set; }

        public DeliveryState State { get; set; }

        public string Adress { get; set; }
    }
}
