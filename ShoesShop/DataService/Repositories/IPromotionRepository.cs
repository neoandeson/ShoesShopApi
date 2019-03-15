using DataService.Infrastructure;
using DataService.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataService.Repositories
{
    public interface IPromotionRepository : IRepository<Promotion>
    {
    }

    public class PromotionRepository : Repository<Promotion>, IPromotionRepository
    {
        public PromotionRepository(ShoesShopContext context) : base(context)
        {

        }
    }
}
