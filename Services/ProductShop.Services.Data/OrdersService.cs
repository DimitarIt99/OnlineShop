namespace ProductShop.Services.Data
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
                State = (DeliveryState)model.State,
            };
            await this.repository.AddAsync(order);
            await this.repository.SaveChangesAsync();
        }

        public IEnumerable<SummaryProductModel> AllMyOrders(string userId, int take, int skip = 0)
        {
            return this.repository
                .All()
                .Where(a => a.Id == userId)
                .OrderBy(a => a.CreatedOn)
                .Select(a => new SummaryProductModel
                {
                    Id = a.Product.Id,
                    ImageUrl = a.Product.ImageUrl,
                    Name = a.Product.Name,
                    Price = a.Product.Price,
                })
                .Skip(skip)
                .Take(take)
                .ToList();
        }

        public bool AlreadyOrdered(string userId, int productId)
            => this.repository
            .AllAsNoTracking()
            .Any(a => a.UserId == userId && a.ProductId == productId);

        public async Task CancellAsync(RemoveOrderViewModel model)
        {
            var orderToCancell = this.repository.All()
                .Where(a => a.Id == model.Id)
                .FirstOrDefault();

            this.repository.Delete(orderToCancell);
            await this.repository.SaveChangesAsync();
        }

        public async Task ChangeState(EditStateViewModel model)
        {
            var order = this.repository.All()
                .Where(a => a.Id == model.Id)
                .FirstOrDefault();

            order.State = (DeliveryState)model.NewState;
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

        public bool OrderIdExists(string id)
            => this.repository.All()
            .Any(a => a.Id == id);

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

            return spacedTexed.ToString();
        }
    }
}
