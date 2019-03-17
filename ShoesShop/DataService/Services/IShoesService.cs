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
        List<ShoesViewModel> GetAll();
        List<ShoesViewModel> GetShoesByName(string input);
        List<ShoesViewModel> GetShoesByBrand(string brandName);
        ShoesViewModel GetShoesById(int id);
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
        private readonly IImageRepository _imageRepository;
        private readonly IMapper _mapper;

        public ShoesService(IShoesRepository shoesRepository, ISizeRepository sizeRepository,
            IShoesHasSizeRepository shoesHasSizeRepository, IMapper mapper,
            IBrandRepository brandRepository, IImageRepository imageRepository)
        {
            this._shoesRepository = shoesRepository;
            this._sizeRepository = sizeRepository;
            this._shoesHasSizeRepository = shoesHasSizeRepository;
            this._brandRepository = brandRepository;
            this._imageRepository = imageRepository;
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

        public List<ShoesViewModel> GetAll()
        {
            List<Shoes> lsShoes = _shoesRepository
                .GetAll()
                .Where(s => s.IsAvaiable == true)
                .Include(s => s.ShoesHasSize)
                .Include(a => a.Brand)
                .ToList();

            List<ShoesViewModel> lsShoesVM = new List<ShoesViewModel>();
            ShoesViewModel shoesVM = null;
            ShoesHasSizeViewModel shoesHasSizeVM = null;
            foreach (var shoes in lsShoes)
            {
                shoesVM = new ShoesViewModel()
                {
                    Id = shoes.Id,
                    BrandId = shoes.BrandId,
                    BrandName = shoes.Brand.Name,
                    Color = shoes.Color,
                    IsAvaiable = shoes.IsAvaiable,
                    Name = shoes.Name,
                    Description = shoes.Description,
                    Price = shoes.Price,
                    Sex = shoes.Sex
                };

                foreach (var shs in shoes.ShoesHasSize)
                {
                    ShoesHasSize shoesHasSize = _shoesHasSizeRepository.GetAll()
                        .Where(h => h.Id == shs.Id).Include(h => h.Size).FirstOrDefault();
                    shoesHasSizeVM = new ShoesHasSizeViewModel()
                    {
                        Id = shoesHasSize.Id,
                        Quantity = shoesHasSize.Quantity,
                        Scale = shoesHasSize.Size.Scale,
                        ShoesId = shoesHasSize.ShoesId,
                        SizeId = shoesHasSize.SizeId
                    };
                    shoesVM.ShoesHasSizes.Add(shoesHasSizeVM);
                }

                List<Image> images = _imageRepository.GetAll().Where(i => i.IsShoes == true && i.OwnId == shoes.Id).ToList();
                shoesVM.Images = images;
                lsShoesVM.Add(shoesVM);
            }
            return lsShoesVM;
        }

        public List<ShoesViewModel> GetShoesByBrand(string brandName)
        {
            List<Shoes> lsShoes = _shoesRepository
                .GetAll()
                .Where(s => s.IsAvaiable == true && s.Brand.Name == brandName)
                .Include(s => s.ShoesHasSize)
                .Include(a => a.Brand)
                .ToList();

            List<ShoesViewModel> lsShoesVM = new List<ShoesViewModel>();
            ShoesViewModel shoesVM = null;
            ShoesHasSizeViewModel shoesHasSizeVM = null;
            foreach (var shoes in lsShoes)
            {
                shoesVM = new ShoesViewModel()
                {
                    Id = shoes.Id,
                    BrandId = shoes.BrandId,
                    BrandName = shoes.Brand.Name,
                    Color = shoes.Color,
                    Name = shoes.Name,
                    Description = shoes.Description,
                    Price = shoes.Price,
                    Sex = shoes.Sex
                };

                foreach (var shs in shoes.ShoesHasSize)
                {
                    ShoesHasSize shoesHasSize = _shoesHasSizeRepository.GetAll()
                        .Where(h => h.Id == shs.Id).Include(h => h.Size).FirstOrDefault();
                    shoesHasSizeVM = new ShoesHasSizeViewModel()
                    {
                        Id = shoesHasSize.Id,
                        Quantity = shoesHasSize.Quantity,
                        Scale = shoesHasSize.Size.Scale,
                        ShoesId = shoesHasSize.ShoesId,
                        SizeId = shoesHasSize.SizeId
                    };
                    shoesVM.ShoesHasSizes.Add(shoesHasSizeVM);
                }

                List<Image> images = _imageRepository.GetAll().Where(i => i.IsShoes == true && i.OwnId == shoes.Id).ToList();
                shoesVM.Images = images;
                lsShoesVM.Add(shoesVM);
            }
            return lsShoesVM;
        }

        public ShoesViewModel GetShoesById(int id)
        {
            Shoes shoes = _shoesRepository.GetAll().Where(s => s.Id == id).Include(s => s.Brand).FirstOrDefault();

            ShoesHasSizeViewModel shoesHasSizeVM = new ShoesHasSizeViewModel();
            ShoesViewModel shoesVM = new ShoesViewModel()
            {
                Id = shoes.Id,
                BrandId = shoes.BrandId,
                BrandName = shoes.Brand.Name,
                Color = shoes.Color,
                Name = shoes.Name,
                Description = shoes.Description,
                Price = shoes.Price,
                Sex = shoes.Sex
            };

            foreach (var shs in shoes.ShoesHasSize)
            {
                ShoesHasSize shoesHasSize = _shoesHasSizeRepository.GetAll()
                    .Where(h => h.Id == shs.Id).Include(h => h.Size).FirstOrDefault();
                shoesHasSizeVM = new ShoesHasSizeViewModel()
                {
                    Id = shoesHasSize.Id,
                    Quantity = shoesHasSize.Quantity,
                    Scale = shoesHasSize.Size.Scale,
                    ShoesId = shoesHasSize.ShoesId,
                    SizeId = shoesHasSize.SizeId
                };
                shoesVM.ShoesHasSizes.Add(shoesHasSizeVM);
            }

            List<Image> images = _imageRepository.GetAll().Where(i => i.IsShoes == true && i.OwnId == shoes.Id).ToList();
            shoesVM.Images = images;
            return shoesVM;
        }

        public List<ShoesViewModel> GetShoesByName(string input)
        {
            List<Shoes> lsShoes = _shoesRepository
                .GetAll()
                .Where(s => s.IsAvaiable == true && s.Name.Contains(input))
                .Include(s => s.ShoesHasSize)
                .Include(a => a.Brand)
                .ToList();

            List<ShoesViewModel> lsShoesVM = new List<ShoesViewModel>();
            ShoesViewModel shoesVM = null;
            ShoesHasSizeViewModel shoesHasSizeVM = null;
            foreach (var shoes in lsShoes)
            {
                shoesVM = new ShoesViewModel()
                {
                    Id = shoes.Id,
                    BrandId = shoes.BrandId,
                    BrandName = shoes.Brand.Name,
                    Color = shoes.Color,
                    Name = shoes.Name,
                    Description = shoes.Description,
                    Price = shoes.Price,
                    Sex = shoes.Sex
                };

                foreach (var shs in shoes.ShoesHasSize)
                {
                    ShoesHasSize shoesHasSize = _shoesHasSizeRepository.GetAll()
                        .Where(h => h.Id == shs.Id).Include(h => h.Size).FirstOrDefault();
                    shoesHasSizeVM = new ShoesHasSizeViewModel()
                    {
                        Id = shoesHasSize.Id,
                        Quantity = shoesHasSize.Quantity,
                        Scale = shoesHasSize.Size.Scale,
                        ShoesId = shoesHasSize.ShoesId,
                        SizeId = shoesHasSize.SizeId
                    };
                    shoesVM.ShoesHasSizes.Add(shoesHasSizeVM);
                }

                List<Image> images = _imageRepository.GetAll().Where(i => i.IsShoes == true && i.OwnId == shoes.Id).ToList();
                shoesVM.Images = images;
                lsShoesVM.Add(shoesVM);
            }
            return lsShoesVM;
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
