using DataService.Infrastructure;
using DataService.Models;
using System.Collections.Generic;

namespace DataService.Repositories
{
    public interface ISizeRepository : IRepository<Size>
    {
    }

    public class SizeRepository : Repository<Size>, ISizeRepository
    {
        public SizeRepository(ShoesShopContext context) : base(context)
        {
        }
    }
}