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
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        //[HttpPost]
        //public IActionResult CreateOrder(OrderViewModel orderViewModel)
        //{
        //    Order order = _orderService.CreateOrder(orderViewModel);

        //    if (order != null)
        //    {
        //        return new JsonResult(order) { StatusCode = StatusCodes.Status200OK };
        //    }

        //    return new JsonResult(order) { StatusCode = StatusCodes.Status409Conflict };
        //}


        [HttpGet]
        public IActionResult GetAllTable()
        {
            List<Order> orders = _orderService.GetOrders();

            return new JsonResult(new
            {
                draw = 1,
                recordsTotal = orders.Count,
                recordsFiltered = orders.Count,
                data = orders
            });
        }

        [HttpGet]
        public IActionResult GetOrder(int id)
        {
            Order order = _orderService.GetOrder(id);

            if (order != null)
            {
                return new JsonResult(order) { StatusCode = StatusCodes.Status200OK };
            }

            return new JsonResult(order) { StatusCode = StatusCodes.Status409Conflict };
        }

        [HttpPost]
        public IActionResult UpdateOrder(OrderViewModel orderViewModel)
        {
            Order order = _orderService.UpdateOrder(orderViewModel);

            if (order != null)
            {
                return new JsonResult(order) { StatusCode = StatusCodes.Status200OK };
            }

            return new JsonResult(order) { StatusCode = StatusCodes.Status409Conflict };
        }

        [HttpPost]
        public IActionResult PutOrderDetail(OrderDetailViewModel orderDetailViewModel)
        {
            bool rs = _orderService.PutOrderDetail(orderDetailViewModel);

            if (rs == true)
            {
                return new JsonResult(rs) { StatusCode = StatusCodes.Status200OK };
            }

            return new JsonResult(rs) { StatusCode = StatusCodes.Status409Conflict };
        }
    }
}