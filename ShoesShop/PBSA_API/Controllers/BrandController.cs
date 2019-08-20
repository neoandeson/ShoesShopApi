using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataService.Models;
using DataService.Services;
using DataService.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Shoes_API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private readonly IBrandService _brandService;

        public BrandController(IBrandService brandService)
        {
            _brandService = brandService;
        }

        [HttpGet]
        public IActionResult Open()
        {
            var str2 = "Open door";
            return new JsonResult(null) { StatusCode = StatusCodes.Status200OK };
        }

        [HttpGet]
        public IActionResult GetAllBrand()
        {
            List<Brand> brands = _brandService.GetAllBrand();

            List<BrandViewModel> brandVMs = new List<BrandViewModel>();
            BrandViewModel brandViewModel = null;
            foreach (var b in brands)
            {
                brandViewModel = new BrandViewModel()
                {
                    Id = b.Id,
                    Name = b.Name
                };

                brandVMs.Add(brandViewModel);
            }

            return new JsonResult(brandVMs) { StatusCode = StatusCodes.Status200OK};
        }

        [HttpGet]
        public IActionResult GetAllTable()
        {
            List<Brand> brands = _brandService.GetAllBrand();

            List<BrandViewModel> brandVMs = new List<BrandViewModel>();
            BrandViewModel brandViewModel = null;
            foreach (var b in brands)
            {
                brandViewModel = new BrandViewModel()
                {
                    Id = b.Id,
                    Name = b.Name
                };

                brandVMs.Add(brandViewModel);
            }

            return new JsonResult(new
            {
                draw = 1,
                recordsTotal = brandVMs.Count,
                recordsFiltered = brandVMs.Count,
                data = brandVMs
            });
        }

        [HttpPost]
        public IActionResult CreateBrand(BrandViewModel brandViewModel)
        {
            Brand brand = _brandService.CreateBrand(brandViewModel);
            return new JsonResult(brand) { StatusCode = StatusCodes.Status200OK };
        }

        [HttpGet]
        public IActionResult GetBrand(int id)
        {
            Brand brand = _brandService.GetBrand(id);
            return new JsonResult(brand) { StatusCode = StatusCodes.Status200OK };
        }

        [HttpPost]
        public IActionResult UpdateBrand(BrandViewModel brandViewModel)
        {
            bool rs = _brandService.UpdateBrand(brandViewModel);
            return new JsonResult(rs) { StatusCode = StatusCodes.Status200OK };
        }

        [HttpPost]
        public IActionResult CleanGlass()
        {
            string str = "Clean";
            return new JsonResult(str) { StatusCode = StatusCodes.Status200OK };
        }
    }
}