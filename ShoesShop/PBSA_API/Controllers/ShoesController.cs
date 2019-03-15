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

        [HttpGet]
        public List<ShoesViewModel> GetAll()
        {
            List<ShoesViewModel> lsShoesVM = _shoesService.GetAll();

            return lsShoesVM;
        }

        [HttpGet]
        public IActionResult GetAllTable()
        {
            List<ShoesViewModel> lsShoesVM = _shoesService.GetAll();

            return new JsonResult(new
            {
                draw = 1,
                recordsTotal = lsShoesVM.Count,
                recordsFiltered = lsShoesVM.Count,
                data = lsShoesVM
            });
        }

        [HttpGet]
        public List<ShoesViewModel> GetShoesByName(string name)
        {
            List<ShoesViewModel> lsShoesVM = _shoesService.GetShoesByName(name);

            return lsShoesVM;
        }

        [HttpGet]
        public ShoesViewModel GetShoesById(int id)
        {
            ShoesViewModel shoesVM = _shoesService.GetShoesById(id);

            return shoesVM;
        }

        [HttpGet]
        public List<ShoesViewModel> GetShoesByBrand(string brandName)
        {
            List<ShoesViewModel> lsShoesVM = _shoesService.GetShoesByBrand(brandName);

            return lsShoesVM;
        }

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