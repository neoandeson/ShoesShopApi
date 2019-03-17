using DataService.Models;
using DataService.Repositories;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using DataService.ViewModels;
using AutoMapper;

namespace DataService.Services
{
    public interface IShoesHasSizeService
    {
        bool UpdateShoesSize(int shoesId, int sizeId, int quantity);
        bool TakeShoesSize(int shoesId, int sizeId, int quantity);
        bool PutShoesSize(int shoesId, int sizeId, int quantity);
        bool AddShoesSize(int shoesId, int sizeId, int quantity);
        List<ShoesHasSizeViewModel> GetShoesHasSizes();
    }

    public class ShoesHasSizeService : IShoesHasSizeService
    {
        private readonly IShoesRepository _shoesRepository;
        private readonly ISizeRepository _sizeRepository;
        private readonly IShoesHasSizeRepository _shoesHasSizeRepository;
        private readonly IMapper _mapper;

        public ShoesHasSizeService(IShoesRepository shoesRepository, ISizeRepository sizeRepository,
            IShoesHasSizeRepository shoesHasSizeRepository, IMapper mapper,
            IBrandRepository brandRepository)
        {
            this._shoesRepository = shoesRepository;
            this._sizeRepository = sizeRepository;
            this._shoesHasSizeRepository = shoesHasSizeRepository;
            this._mapper = mapper;
        }

        public bool AddShoesSize(int shoesId, int sizeId, int quantity)
        {
            ShoesHasSize shoesHasSize = new ShoesHasSize() {
                ShoesId = shoesId,
                SizeId = sizeId,
                Quantity = quantity
            };

            _shoesHasSizeRepository.Add(shoesHasSize);

            return true;
        }

        public List<ShoesHasSizeViewModel> GetShoesHasSizes()
        {
            List<ShoesHasSize> shoesHasSizes = _shoesHasSizeRepository.GetAll().Include(s => s.Shoes).Include(s => s.Size).ToList();
            List<ShoesHasSizeViewModel> shoesHasSizeVMs = new List<ShoesHasSizeViewModel>();
            ShoesHasSizeViewModel shoesHasSizeVM = null;
            foreach (var shs in shoesHasSizes)
            {
                shoesHasSizeVM = new ShoesHasSizeViewModel()
                {
                    Id = shs.Id,
                    Quantity = shs.Quantity,
                    Scale = shs.Size.Scale,
                    ShoesId = shs.ShoesId,
                    ShoesName = shs.Shoes.Name,
                    SizeId = shs.SizeId
                };

                shoesHasSizeVMs.Add(shoesHasSizeVM);
            }

            return shoesHasSizeVMs;
        }

        public bool PutShoesSize(int shoesId, int sizeId, int quantity)
        {
            ShoesHasSize shoesHasSize = _shoesHasSizeRepository
                .GetAll()
                .Where(s => s.ShoesId == shoesId && s.SizeId == sizeId)
                .FirstOrDefault();
            if (shoesHasSize != null)
            {
                shoesHasSize.Quantity += quantity;
                _shoesHasSizeRepository.Update(shoesHasSize);
                return true;
            }
            return false;
        }

        public bool TakeShoesSize(int shoesId, int sizeId, int quantity)
        {
            ShoesHasSize shoesHasSize = _shoesHasSizeRepository
                .GetAll()
                .Where(s => s.ShoesId == shoesId && s.SizeId == sizeId)
                .FirstOrDefault();
            if (shoesHasSize != null)
            {
                if(shoesHasSize.Quantity < quantity)
                {
                    return false;
                }

                shoesHasSize.Quantity -= quantity;
                _shoesHasSizeRepository.Update(shoesHasSize);
                return true;
            }
            return false;
        }

        public bool UpdateShoesSize(int shoesId, int sizeId, int quantity)
        {
            ShoesHasSize shoesHasSize = _shoesHasSizeRepository
                .GetAll()
                .Where(s => s.ShoesId == shoesId && s.SizeId == sizeId)
                .FirstOrDefault();
            if (shoesHasSize != null)
            {
                shoesHasSize.Quantity = quantity;
                _shoesHasSizeRepository.Update(shoesHasSize);

                if(quantity == 0)
                {
                    Shoes shoes = _shoesRepository.GetById(shoesId);
                    shoes.IsAvaiable = false;
                    _shoesRepository.Update(shoes);
                } else
                {
                    Shoes shoes = _shoesRepository.GetById(shoesId);
                    shoes.IsAvaiable = true;
                    _shoesRepository.Update(shoes);
                }
                return true;
            }

            return false;
        }
    }
}
