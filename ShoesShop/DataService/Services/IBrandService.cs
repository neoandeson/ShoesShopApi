using DataService.Models;
using DataService.Repositories;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using DataService.ViewModels;
using AutoMapper;

namespace DataService.Services
{
    public interface IBrandService
    {
        List<Brand> GetAllBrand();
        Brand CreateBrand(BrandViewModel brandViewModel);
    }

    public class BrandService : IBrandService
    {
        private readonly IBrandRepository _brandRepository;

        public BrandService(IBrandRepository brandRepository)
        {
            this._brandRepository = brandRepository;
        }

        public Brand CreateBrand(BrandViewModel brandViewModel)
        {
            Brand brand = _brandRepository.Add(new Brand() {
                Name = brandViewModel.Name,
            });

            return brand;
        }

        public List<Brand> GetAllBrand()
        {
            List<Brand> brands = _brandRepository.GetAll().ToList();

            return brands;
        }
    }
}
