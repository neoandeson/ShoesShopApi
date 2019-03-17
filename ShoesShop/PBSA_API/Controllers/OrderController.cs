using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
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
        private readonly IMapper _mapper;

        public OrderController(IOrderService orderService, IMapper mapper)
        {
            _orderService = orderService;
            _mapper = mapper;
        }

        [HttpPost]
        public IActionResult CreateOrder(OrderAddViewModel orderAddViewModel)
        {
            Order order = _orderService.CreateOrder(orderAddViewModel);
            OrderViewModel orderVM = _mapper.Map<OrderViewModel>(order);

            if (order != null)
            {
                return new JsonResult(orderVM) { StatusCode = StatusCodes.Status200OK };
            }

            return new JsonResult(orderVM) { StatusCode = StatusCodes.Status409Conflict };
        }


        [HttpGet]
        public IActionResult GetAllTable()
        {
            List<Order> orders = _orderService.GetOrders();
            List<OrderViewModel> orderVMs = _mapper.Map<List<OrderViewModel>>(orders);
            return new JsonResult(new
            {
                draw = 1,
                recordsTotal = orderVMs.Count,
                recordsFiltered = orderVMs.Count,
                data = orderVMs
            });
        }

        [HttpGet]
        public IActionResult GetOrder(int id)
        {
            Order order = _orderService.GetOrder(id);
            OrderInfoViewModel orderVM = _mapper.Map<OrderInfoViewModel>(order);
            if (order != null)
            {
                return new JsonResult(orderVM) { StatusCode = StatusCodes.Status200OK };
            }

            return new JsonResult(orderVM) { StatusCode = StatusCodes.Status409Conflict };
        }

        [HttpPost]
        public IActionResult UpdateOrder(OrderViewModel orderViewModel)
        {
            Order order = _orderService.UpdateOrder(orderViewModel);
            OrderViewModel orderVM = _mapper.Map<OrderViewModel>(order);
            if (order != null)
            {
                return new JsonResult(orderVM) { StatusCode = StatusCodes.Status200OK };
            }

            return new JsonResult(orderVM) { StatusCode = StatusCodes.Status409Conflict };
        }

        //[HttpPost]
        //public IActionResult PutOrderDetail(OrderDetailViewModel orderDetailViewModel)
        //{
        //    bool rs = _orderService.PutOrderDetail(orderDetailViewModel);

        //    if (rs == true)
        //    {
        //        return new JsonResult(rs) { StatusCode = StatusCodes.Status200OK };
        //    }

        //    return new JsonResult(rs) { StatusCode = StatusCodes.Status409Conflict };
        //}
    }
}