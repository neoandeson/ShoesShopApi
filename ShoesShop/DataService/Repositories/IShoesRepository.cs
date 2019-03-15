using DataService.Infrastructure;
using DataService.Models;
using System.Collections.Generic;

namespace DataService.Repositories
{
    public interface IShoesRepository : IRepository<Shoes>
    {
    }

    public class ShoesRepository : Repository<Shoes>, IShoesRepository
    {
        public ShoesRepository(ShoesShopContext context) : base(context)
        {
        }
    }
}