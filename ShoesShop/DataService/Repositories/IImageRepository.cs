using DataService.Infrastructure;
using DataService.Models;
using System.Collections.Generic;

namespace DataService.Repositories
{
    public interface IImageRepository : IRepository<Image>
    {
    }

    public class ImageRepository : Repository<Image>, IImageRepository
    {
        public ImageRepository(ShoesShopContext context) : base(context)
        {
        }
    }
}