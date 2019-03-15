using DataService.Infrastructure;
using DataService.Models;
using System.Collections.Generic;

namespace DataService.Repositories
{
    public interface IOrderDetailRepository : IRepository<OrderDetail>
    {
    }

    public class OrderDetailRepository : Repository<OrderDetail>, IOrderDetailRepository
    {
        public OrderDetailRepository(ShoesShopContext context) : base(context)
        {
        }
    }
}