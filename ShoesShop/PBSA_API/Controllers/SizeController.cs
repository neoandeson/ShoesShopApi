using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataService.Models;
using DataService.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Shoes_API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SizeController : ControllerBase
    {
        private readonly ISizeService _sizeService;

        public SizeController(ISizeService sizeService)
        {
            _sizeService = sizeService;
        }

        [HttpGet]
        public IActionResult GetAllSize()
        {
            List<Size> sizes = _sizeService.GetAllSize();

            if (sizes != null)
            {
                return new JsonResult(sizes) { StatusCode = StatusCodes.Status200OK };
            }

            return new JsonResult(sizes) { StatusCode = StatusCodes.Status409Conflict };
        }

        [HttpGet]
        public IActionResult GetAllTable()
        {
            List<Size> sizes = _sizeService.GetAllSize();

            return new JsonResult(new
            {
                draw = 1,
                recordsTotal = sizes.Count,
                recordsFiltered = sizes.Count,
                data = sizes
            });
        }

        [HttpPost]
        public IActionResult InitSize()
        {
            _sizeService.InitSize();

             return new JsonResult(null) { StatusCode = StatusCodes.Status200OK };
        }
    }
}