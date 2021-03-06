﻿namespace ProductShop.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using ProductShop.Data.Common.Repositories;
    using ProductShop.Data.Models;
    using ProductShop.Data.Models.Enums;
    using ProductShop.Web.ViewModels.Orders;
    using ProductShop.Web.ViewModels.Products;

    public class OrdersService : IOrdersService
    {
        private readonly IDeletableEntityRepository<Order> repository;

        public OrdersService(IDeletableEntityRepository<Order> repository)
        {
            this.repository = repository;
        }

        public async Task AddOrderAsync(CreateOrderViewModel model)
        {
            var order = new Order
            {
                UserId = model.UserId,
                ProductId = model.ProductId,
                Adress = model.Adress,
                State = DeliveryState.Ordered,
            };
            await this.repository.AddAsync(order);
            await this.repository.SaveChangesAsync();
        }

        public IEnumerable<OrderSummaryViewModel> AllOrdersByUserId(string userId, int take, int skip = 0)
        {
            var ordersList = this.repository
                .All()
                .Where(a => a.UserId == userId)
                .Select(a => new OrderSummaryViewModel
                {
                    Id = a.Id,
                    ProductId = a.Product.Id,
                    ImageUrl = a.Product.ImageUrl,
                    Name = a.Product.Name,
                    Price = a.Product.Price,
                    AverageRating = (decimal)a.Product.Ratings.Average(a => (int)a.Grade),
                })
                .Skip(skip)
                .Take(take)
                .ToList();
            return ordersList;
        }

        public bool AlreadyOrdered(string userId, int productId)
            => this.repository
            .AllAsNoTracking()
            .Any(a => a.UserId == userId && a.ProductId == productId);

        public async Task<int> CancelAsync(RemoveOrderViewModel model)
        {
            var orderToCancell = this.repository.All()
                .Where(a => a.Id == model.Id)
                .FirstOrDefault();
            var product = this.repository.All().Where(a => a.Id == model.Id)
                .Select(a => a.ProductId)
                .FirstOrDefault();
            this.repository.Delete(orderToCancell);
            await this.repository.SaveChangesAsync();

            return product;
        }

        public async Task ChangeStateAsync(EditStateViewModel model)
        {
            var order = this.repository.All()
                .Where(a => a.Id == model.Id)
                .FirstOrDefault();
            order.State = (DeliveryState)model.NewState;
            if ((DeliveryState)model.NewState == DeliveryState.Received)
            {
                this.repository.Delete(order);
            }

            await this.repository.SaveChangesAsync();
        }

        public DelivaryStateViewModel GetOrderDelivaryState(string id)
        {
            var state = this.repository.All().Where(a => a.Id == id)
                   .Select(a => new DelivaryStateViewModel
                   {
                       State = a.State,
                   })
                   .FirstOrDefault();
            state.StateSpacedOut = this.AddingSpacesBetweenUpperCaseStrings(state.State.ToString());

            return state;
        }

        public bool IdExists(string id)
            => this.repository.AllAsNoTracking().Any(a => a.Id == id);

        public int OrderedProductsCount(string userId)
            => this.repository
            .AllAsNoTracking()
            .Where(a => a.UserId == userId)
            .Count();

        private string AddingSpacesBetweenUpperCaseStrings(string originalState)
        {
            var spacedTexed = new StringBuilder();

            foreach (var ch in originalState)
            {
                if (char.IsLower(ch))
                {
                    spacedTexed.Append(ch);
                }
                else
                {
                    spacedTexed.Append(" ");
                    spacedTexed.Append(ch);
                }
            }

            return spacedTexed.ToString().Trim();
        }
    }
}
