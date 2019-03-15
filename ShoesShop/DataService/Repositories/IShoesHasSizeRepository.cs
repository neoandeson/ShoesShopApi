using DataService.Infrastructure;
using DataService.Models;
using System.Collections.Generic;

namespace DataService.Repositories
{
    public interface IShoesHasSizeRepository : IRepository<ShoesHasSize>
    {
    }

    public class ShoesHasSizeRepository : Repository<ShoesHasSize>, IShoesHasSizeRepository
    {
        public ShoesHasSizeRepository(ShoesShopContext context) : base(context)
        {
        }
    }
}