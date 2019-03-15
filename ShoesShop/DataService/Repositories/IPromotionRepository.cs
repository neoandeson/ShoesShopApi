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

    public class BarberRepository : Repository<Promotion>, IPromotionRepository
    {
        public BarberRepository(ShoesShopContext context) : base(context)
        {

        }
    }
}
