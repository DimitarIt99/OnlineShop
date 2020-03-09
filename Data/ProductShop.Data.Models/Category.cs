﻿namespace ProductShop.Data.Models
{
    using ProductShop.Data.Common.Models;

    public class Category : BaseModel<int>
    {
        public string Name { get; set; }

        public string Description { get; set; }
    }
}
