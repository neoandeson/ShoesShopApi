using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataService.Services;
using DataService.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Shoes_API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ShoesHasSizeController : ControllerBase
    {
        private readonly IShoesHasSizeService _shoesHasSizeService;

        public ShoesHasSizeController(IShoesHasSizeService shoesHasSizeService)
        {
            _shoesHasSizeService = shoesHasSizeService;
        }

        [HttpPost]
        public IActionResult PutShoesSize(int shoesId, int sizeId, int quantity)
        {
            bool rs = _shoesHasSizeService.PutShoesSize(shoesId, sizeId, quantity);

            if (rs == true)
            {
                return new JsonResult(rs) { StatusCode = StatusCodes.Status200OK };
            }

            return new JsonResult(rs) { StatusCode = StatusCodes.Status409Conflict };
        }

        [HttpPost]
        public IActionResult AddShoesSize(int shoesId, int sizeId, int quantity)
        {
            bool rs = _shoesHasSizeService.AddShoesSize(shoesId, sizeId, quantity);

            if (rs == true)
            {
                return new JsonResult(rs) { StatusCode = StatusCodes.Status200OK };
            }

            return new JsonResult(rs) { StatusCode = StatusCodes.Status409Conflict };
        }

        [HttpPost]
        public IActionResult TakeShoesSize(int shoesId, int sizeId, int quantity)
        {
            bool rs = _shoesHasSizeService.TakeShoesSize(shoesId, sizeId, quantity);

            if (rs == true)
            {
                return new JsonResult(rs) { StatusCode = StatusCodes.Status200OK };
            }

            return new JsonResult(rs) { StatusCode = StatusCodes.Status409Conflict };
        }

        [HttpPost]
        public IActionResult UpdateShoesSize(int shoesId, int sizeId, int quantity)
        {
            bool rs = _shoesHasSizeService.UpdateShoesSize(shoesId, sizeId, quantity);

            if (rs == true)
            {
                return new JsonResult(rs) { StatusCode = StatusCodes.Status200OK };
            }

            return new JsonResult(rs) { StatusCode = StatusCodes.Status409Conflict };
        }

        [HttpGet]
        public IActionResult GetAllTable()
        {
            List<ShoesHasSizeViewModel> shoesHasSizeVMs = _shoesHasSizeService.GetShoesHasSizes();

            return new JsonResult(shoesHasSizeVMs) { StatusCode = StatusCodes.Status200OK };
        }

        [HttpGet]
        public IActionResult GetShoesHasSize(int id)
        {
            ShoesHasSizeViewModel shoesHasSizeVM = _shoesHasSizeService.GetShoesHasSize(id);
            if (shoesHasSizeVM != null)
            {
                return new JsonResult(shoesHasSizeVM) { StatusCode = StatusCodes.Status200OK };
            }
            return new JsonResult(null) { StatusCode = StatusCodes.Status409Conflict };
        }
    }
}