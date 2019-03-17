using DataService.Models;
using DataService.Repositories;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using DataService.ViewModels;
using AutoMapper;

namespace DataService.Services
{
    public interface IOrderDetailService
    {
        List<OrderDetail> GetOrderDetails(int orderId);
    }

    public class OrderDetailService : IOrderDetailService
    {
        private readonly IOrderDetailRepository _orderDetailRepository;
        private readonly IShoesRepository _shoesRepository;
        private readonly ISizeRepository _sizeRepository;

        public OrderDetailService(IOrderDetailRepository orderDetailRepository, IShoesRepository shoesRepository, ISizeRepository sizeRepository)
        {
            _orderDetailRepository = orderDetailRepository;
            _shoesRepository = shoesRepository;
            _sizeRepository = sizeRepository;
        }

        public List<OrderDetail> GetOrderDetails(int orderId)
        {
            List<OrderDetail> orderDetails = _orderDetailRepository
                .GetAll()
                .Where(od => od.OrderId == orderId)
                .ToList();
            foreach (var od in orderDetails)
            {
                od.Shoes = _shoesRepository.GetById(od.ShoesId.Value);
                od.Shoes.OrderDetail = null;
                od.Size = _sizeRepository.GetById(od.SizeId.Value);
                od.Size.OrderDetail = null;
            }

            return orderDetails;
        }
    }
}
