using DataService.Infrastructure;
using DataService.Models;
using System.Collections.Generic;

namespace DataService.Repositories
{
    public interface IBrandRepository : IRepository<Brand>
    {
    }

    public class BrandRepository : Repository<Brand>, IBrandRepository
    {
        public BrandRepository(ShoesShopContext context) : base(context)
        {
        }
    }
}