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

        [HttpPost]
        public IActionResult CreateBrand(BrandViewModel brandViewModel)
        {
            Brand brands = _brandService.CreateBrand(brandViewModel);
            return new JsonResult(brands) { StatusCode = StatusCodes.Status200OK };
        }
    }
}