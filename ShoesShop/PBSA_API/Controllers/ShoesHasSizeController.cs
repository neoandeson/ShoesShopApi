using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataService.Services;
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
    }
}