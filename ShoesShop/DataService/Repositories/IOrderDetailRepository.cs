using DataService.Infrastructure;
using DataService.Models;
using System.Collections.Generic;

namespace DataService.Repositories
{
    public interface IOrderRepository : IRepository<Order>
    {
    }

    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        public OrderRepository(ShoesShopContext context) : base(context)
        {
        }
    }
}