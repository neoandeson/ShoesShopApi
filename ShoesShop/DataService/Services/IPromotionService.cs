using DataService.Models;
using DataService.Repositories;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace DataService.Services
{
    public interface IPromotionService
    {
        List<Promotion> GetAll();
    }

    public class PromotionService : IPromotionService
    {
        private readonly IPromotionRepository _promotionRepository;      

        public List<Promotion> GetAll()
        {
            List<Promotion> promotions = _promotionRepository.GetAll().ToList();
            return promotions;
        }

    }
}
