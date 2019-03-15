using DataService.Models;
using DataService.Repositories;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using DataService.ViewModels;
using AutoMapper;

namespace DataService.Services
{
    public interface IShoesService
    {
        List<Shoes> GetAll();
        List<Shoes> GetShoesByName(string input);
        List<Shoes> GetShoesByBrand(string brandName);
        Shoes GetShoesById(int id);
        bool AddShoes(ShoesAddViewModel shoesAddVM);
        bool Update(ShoesViewModel shoesVM);
        bool DeleteShoes(int id);
    }

    public class ShoesService : IShoesService
    {
        private readonly IShoesRepository _shoesRepository;
        private readonly ISizeRepository _sizeRepository;
        private readonly IShoesHasSizeRepository _shoesHasSizeRepository;
        private readonly IBrandRepository _brandRepository;
        private readonly IMapper _mapper;

        public ShoesService(IShoesRepository shoesRepository, ISizeRepository sizeRepository,
            IShoesHasSizeRepository shoesHasSizeRepository, IMapper mapper,
            IBrandRepository brandRepository)
        {
            this._shoesRepository = shoesRepository;
            this._sizeRepository = sizeRepository;
            this._shoesHasSizeRepository = shoesHasSizeRepository;
            this._brandRepository = brandRepository;
            this._mapper = mapper;
        }

        public bool AddShoes(ShoesAddViewModel shoesVM)
        {
            //Shoes shoes = _mapper.Map<Shoes>(shoesVM);
            Brand brand = _brandRepository.GetById(shoesVM.BrandId);
            if(brand != null)
            {
                Shoes shoes = new Shoes()
                {
                    Name = shoesVM.Name,
                    Brand = brand,
                    Color = shoesVM.Color,
                    Avatar1 = shoesVM.Avatar1,
                    Avatar2 = shoesVM.Avatar2,
                    IsAvaiable = true,
                    Description = shoesVM.Description,
                    Price = shoesVM.Price,
                    Sex = shoesVM.Sex
                };

                _shoesRepository.Add(shoes);
                return true;
            }
            return false;
        }

        public bool DeleteShoes(int id)
        {
            Shoes shoes = _shoesRepository.GetById(id);
            if(shoes != null)
            {
                _shoesRepository.Delete(shoes);
                return true;
            }
            return false;
        }

        public List<Shoes> GetAll()
        {
            List<Shoes> lsShoes = _shoesRepository
                .GetAll()
                .Include(s => s.ShoesHasSize)
                .Include(a => a.Brand)
                .ToList();

            IEnumerable<ShoesHasSize> shoesHasSizes = _shoesHasSizeRepository
                .GetAll()
                .Include(s => s.Size);

            foreach (var shoes in lsShoes)
            {
                foreach (var sshs in shoes.ShoesHasSize)
                {
                    foreach (var shs in shoesHasSizes)
                    {
                        if(sshs.Id == shs.Id)
                        {
                            sshs.Size = shs.Size;
                        }
                    }
                }
            }
            return lsShoes;
        }

        public List<Shoes> GetShoesByBrand(string brandName)
        {
            List<Shoes> lsShoes = _shoesRepository
                .GetAll()
                .Where(s => s.Brand.Name == brandName)
                .Include(s => s.ShoesHasSize)
                .Include(a => a.Brand)
                .ToList();

            IEnumerable<ShoesHasSize> shoesHasSizes = _shoesHasSizeRepository
                .GetAll()
                .Include(s => s.Size);

            foreach (var shoes in lsShoes)
            {
                foreach (var sshs in shoes.ShoesHasSize)
                {
                    foreach (var shs in shoesHasSizes)
                    {
                        if (sshs.Id == shs.Id)
                        {
                            sshs.Size = shs.Size;
                        }
                    }
                }
            }
            return lsShoes;
        }

        public Shoes GetShoesById(int id)
        {
            Shoes shoes = _shoesRepository.GetById(id);
            return shoes;
        }

        public List<Shoes> GetShoesByName(string input)
        {
            List<Shoes> lsShoes = _shoesRepository
                .GetAll()
                .Where(s => s.Name.Contains(input))
                .Include(s => s.ShoesHasSize)
                .Include(a => a.Brand)
                .ToList();

            IEnumerable<ShoesHasSize> shoesHasSizes = _shoesHasSizeRepository
                .GetAll()
                .Include(s => s.Size);

            foreach (var shoes in lsShoes)
            {
                foreach (var sshs in shoes.ShoesHasSize)
                {
                    foreach (var shs in shoesHasSizes)
                    {
                        if (sshs.Id == shs.Id)
                        {
                            sshs.Size = shs.Size;
                        }
                    }
                }
            }
            return lsShoes;
        }

        public bool Update(ShoesViewModel shoesVM)
        {
            Brand brand = _brandRepository.GetById(shoesVM.BrandId);
            if (brand != null)
            {
                Shoes shoes = new Shoes()
                {
                    Name = shoesVM.Name,
                    Brand = brand,
                    Color = shoesVM.Color,
                    Avatar1 = shoesVM.Avatar1,
                    Avatar2 = shoesVM.Avatar2,
                    IsAvaiable = true,
                    Description = shoesVM.Description,
                    Price = shoesVM.Price,
                    Sex = shoesVM.Sex
                };

                _shoesRepository.Update(shoes);
                return true;
            }
            return false;
        }
    }
}
