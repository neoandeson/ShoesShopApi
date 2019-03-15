using System.Collections.Generic;
using AutoMapper;
using DataService.Models;
using DataService.Services;
using DataService.ViewModels;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PBSA_API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ShoesController : ControllerBase
    {
        private readonly IShoesService _shoesService;
        private readonly IMapper _mapper;

        public ShoesController(IShoesService shoesService, IMapper mapper)
        {
            this._shoesService = shoesService;
            this._mapper = mapper;
        }

        [EnableCors("AllowMyOrigin")]
        [HttpGet]
        public List<ShoesViewModel> GetAll()
        {
            List<Shoes> lsShoes = _shoesService.GetAll();

            List<ShoesViewModel> lsShoesVM = _mapper.Map< List<ShoesViewModel>>(lsShoes);
            
            return lsShoesVM;
        }

        [EnableCors("AllowMyOrigin")]
        [HttpGet]
        public List<ShoesViewModel> GetShoesByName(string name)
        {
            List<Shoes> lsShoes = _shoesService.GetShoesByName(name);

            List<ShoesViewModel> lsShoesVM = _mapper.Map<List<ShoesViewModel>>(lsShoes);

            return lsShoesVM;
        }

        [EnableCors("AllowMyOrigin")]
        [HttpGet]
        public ShoesViewModel GetShoesById(int id)
        {
            Shoes shoes = _shoesService.GetShoesById(id);

            ShoesViewModel lsShoesVM = _mapper.Map<ShoesViewModel>(shoes);

            return lsShoesVM;
        }

        [EnableCors("AllowMyOrigin")]
        [HttpGet]
        public List<ShoesViewModel> GetShoesByBrand(string brandName)
        {
            List<Shoes> lsShoes = _shoesService.GetShoesByBrand(brandName);

            List<ShoesViewModel> lsShoesVM = _mapper.Map<List<ShoesViewModel>>(lsShoes);

            return lsShoesVM;
        }

        [EnableCors("AllowMyOrigin")]
        [HttpPost]
        public IActionResult AddShoes(ShoesAddViewModel shoesAddViewModel)
        {
            bool result = _shoesService.AddShoes(shoesAddViewModel);

            if(result == true)
            {
                return new JsonResult(result) { StatusCode = StatusCodes.Status200OK };
            }

            return new JsonResult(result) { StatusCode = StatusCodes.Status409Conflict };
        }

        [EnableCors("AllowMyOrigin")]
        [HttpPost]
        public IActionResult Delete(int shoesId)
        {
            bool result = _shoesService.DeleteShoes(shoesId);

            if (result == true)
            {
                return new JsonResult(result) { StatusCode = StatusCodes.Status200OK };
            }

            return new JsonResult(result) { StatusCode = StatusCodes.Status409Conflict };
        }

        [EnableCors("AllowMyOrigin")]
        [HttpPost]
        public IActionResult Update(ShoesViewModel shoesViewModel)
        {
            bool result = _shoesService.Update(shoesViewModel);

            if (result == true)
            {
                return new JsonResult(result) { StatusCode = StatusCodes.Status200OK };
            }

            return new JsonResult(result) { StatusCode = StatusCodes.Status409Conflict };
        }
    }
}