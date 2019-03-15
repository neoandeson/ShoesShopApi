using DataService.Models;
using DataService.Repositories;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using DataService.ViewModels;
using AutoMapper;
using System;

namespace DataService.Services
{
    public interface IOrderService
    {
        Order CreateOrder(OrderViewModel orderViewModel);
        Order GetOrder(int id);
        Order UpdateOrder(OrderViewModel orderViewModel);
        bool PutOrderDetail(OrderDetailViewModel orderDetailViewModel);
    }

    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderDetailRepository _orderDetailRepository;

        public OrderService(IOrderRepository orderRepository, IOrderDetailRepository orderDetailRepository)
        {
            this._orderRepository = orderRepository;
            this._orderDetailRepository = orderDetailRepository;
        }

        public Order CreateOrder(OrderViewModel orderViewModel)
        {
            Order order = new Order()
            {
                ContactPhone = orderViewModel.ContactPhone,
                CreateDate = DateTime.Now,
                CusName = orderViewModel.CusName,
                Description = orderViewModel.Description,
                ShipAddress = orderViewModel.ShipAddress,
                ShipDate = orderViewModel.ShipDate,
                State = orderViewModel.State,
                DiscountCode = orderViewModel.DiscountCode,
                Discount = orderViewModel.Discount,
                Total = orderViewModel.Total
            };
            order = _orderRepository.Add(order);

            return order;
        }

        public Order GetOrder(int id)
        {
            Order order = _orderRepository.GetAll().Where(o => o.Id == id).Include(o => o.OrderDetail).FirstOrDefault();
            return order;
        }

        public bool PutOrderDetail(OrderDetailViewModel orderDetailViewModel)
        {
            OrderDetail orderDetail = _orderDetailRepository
                .GetAll()
                .Where(o => o.OrderId == orderDetailViewModel.OrderId && o.ShoesId == orderDetailViewModel.ShoesId)
                .FirstOrDefault();

            if (orderDetail != null)
            {
                orderDetail.Quantity = orderDetailViewModel.Quantity;
                _orderDetailRepository.Update(orderDetail);
                return true;
            }
            else if (orderDetail == null)
            {
                orderDetail = new OrderDetail()
                {
                    ShoesId = orderDetailViewModel.ShoesId,
                    OrderId = orderDetailViewModel.OrderId,
                    Quantity = orderDetailViewModel.Quantity
                };
                _orderDetailRepository.Add(orderDetail);
                return true;
            }
            return false;
        }

        public Order UpdateOrder(OrderViewModel orderViewModel)
        {
            Order order = _orderRepository.GetById(orderViewModel.Id);
            order.ContactPhone = orderViewModel.ContactPhone;
            order.CreateDate = DateTime.Now;
            order.CusName = orderViewModel.CusName;
            order.Description = orderViewModel.Description;
            order.ShipAddress = orderViewModel.ShipAddress;
            order.ShipDate = orderViewModel.ShipDate;
            order.State = orderViewModel.State;
            order.DiscountCode = orderViewModel.DiscountCode;
            order.Discount = orderViewModel.Discount;
            order.Total = orderViewModel.Total;

            order = _orderRepository.Update(order);
            return order;
        }
    }
}
