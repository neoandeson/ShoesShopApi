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
        Order CreateOrder(OrderAddViewModel orderViewModel);
        Order GetOrder(int id);
        Order UpdateOrder(OrderViewModel orderViewModel);
        List<Order> GetOrders();
        //bool PutOrderDetail(OrderDetailViewModel orderDetailViewModel);
    }

    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderDetailRepository _orderDetailRepository;
        private readonly IShoesHasSizeRepository _shoesHasSizeRepository;
        private readonly IShoesRepository _shoesRepository;
        private readonly IPromotionRepository _promotionRepository;
        private readonly IOrderDetailService _orderDetailService;

        public OrderService(IOrderRepository orderRepository, IOrderDetailRepository orderDetailRepository, 
            IShoesHasSizeRepository shoesHasSizeRepository, IShoesRepository shoesRepository, 
            IPromotionRepository promotionRepository, IOrderDetailService orderDetailService)
        {
            _orderRepository = orderRepository;
            _orderDetailRepository = orderDetailRepository;
            _shoesHasSizeRepository = shoesHasSizeRepository;
            _shoesRepository = shoesRepository;
            _promotionRepository = promotionRepository;
            _orderDetailService = orderDetailService;
        }

        public Order CreateOrder(OrderAddViewModel orderAddVM)
        {
            Order order = new Order()
            {
                ContactPhone = orderAddVM.ContactPhone,
                CreateDate = DateTime.Now,
                CusName = orderAddVM.CusName,
                Description = orderAddVM.Description,
                ShipAddress = orderAddVM.ShipAddress,
                ShipDate = orderAddVM.ShipDate,
                State = "pending",
                DiscountCode = orderAddVM.DiscountCode,
                Sum = 0,
                Total = 0
            };
            order = _orderRepository.Add(order);

            OrderDetail orderDetail = null;
            foreach (var odd in orderAddVM.OrderDetailAdds)
            {
                ShoesHasSize shoesHasSize = _shoesHasSizeRepository.GetAll()
                    .Where(h => h.SizeId == odd.SizeId && h.ShoesId == odd.ShoesId).FirstOrDefault();
                if (shoesHasSize != null)
                {
                    if (shoesHasSize.Quantity >= odd.Quantity)
                    {
                        orderDetail = new OrderDetail()
                        {
                            OrderId = order.Id,
                            Quantity = odd.Quantity,
                            ShoesId = odd.ShoesId,
                            SizeId = odd.SizeId
                        };

                        _orderDetailRepository.Add(orderDetail);

                        double price = _shoesRepository.GetById(odd.ShoesId.Value).Price.Value;
                        order.Sum += price * odd.Quantity;
                        shoesHasSize.Quantity -= odd.Quantity.Value;
                        _shoesHasSizeRepository.Update(shoesHasSize);

                        if(shoesHasSize.Quantity == 0)
                        {
                            Shoes shoes = _shoesRepository.GetById(odd.ShoesId.Value);
                            shoes.IsAvaiable = false;
                            _shoesRepository.Update(shoes);
                        }
                    }
                }
            }

            order.Total = order.Sum;
            if (order.DiscountCode != "")
            {
                order.Discount = _promotionRepository.GetAll()
                    .Where(p => p.DiscountCode == order.DiscountCode).FirstOrDefault().Discount;
                order.Total -= order.Sum * order.Discount / 100;
            }

            if (order.Total == 0)
            {
                _orderRepository.Delete(order);
                return null;
            }
            else
            {
                order = _orderRepository.Update(order);
            }

            return order;
        }

        public Order GetOrder(int id)
        {
            Order order = _orderRepository
                .GetAll()
                .Where(o => o.Id == id)
                .Include(o => o.OrderDetail)
                .FirstOrDefault();

            order.OrderDetail = _orderDetailService.GetOrderDetails(order.Id);

            return order;
        }

        public List<Order> GetOrders()
        {
            return _orderRepository.GetAll().ToList();
        }

        //public bool PutOrderDetail(OrderDetailViewModel orderDetailViewModel)
        //{
        //    OrderDetail orderDetail = _orderDetailRepository
        //        .GetAll()
        //        .Where(o => o.OrderId == orderDetailViewModel.OrderId && o.ShoesId == orderDetailViewModel.ShoesId)
        //        .FirstOrDefault();

        //    if (orderDetail != null)
        //    {
        //        orderDetail.Quantity = orderDetailViewModel.Quantity;
        //        _orderDetailRepository.Update(orderDetail);
        //        return true;
        //    }
        //    else if (orderDetail == null)
        //    {
        //        orderDetail = new OrderDetail()
        //        {
        //            ShoesId = orderDetailViewModel.ShoesId,
        //            OrderId = orderDetailViewModel.OrderId,
        //            Quantity = orderDetailViewModel.Quantity
        //        };
        //        _orderDetailRepository.Add(orderDetail);
        //        return true;
        //    }
        //    return false;
        //}

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
            //order.DiscountCode = orderViewModel.DiscountCode;
            order.Discount = orderViewModel.Discount;
            order.Total = orderViewModel.Total;

            order = _orderRepository.Update(order);
            return order;
        }
    }
}
