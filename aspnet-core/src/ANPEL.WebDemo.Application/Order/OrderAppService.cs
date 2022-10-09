﻿using ANPEL.WebDemo.EntityFrameworkCore;
using ANPEL.WebDemo.Product;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Guids;
using Volo.Abp.Uow;
using Volo.Abp.Users;

namespace ANPEL.WebDemo.Order
{
    [Dependency(ServiceLifetime.Transient)]
    public class OrderAppService : /*WebDemoAppService, */IOrderAppService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly WebDemoDbContext _webDemoDbContext;
        private Volo.Abp.ObjectMapping.IObjectMapper _objectMapper;
        private ICurrentUser _currentUser;
        private IGuidGenerator _guidGenerator;
        private readonly IProductRepository _productRepository;
        private IUnitOfWork _unitOfWork;
        public OrderAppService(IOrderRepository orderRepository, WebDemoDbContext webDemoDbContext, Volo.Abp.ObjectMapping.IObjectMapper objectMapper, ICurrentUser currentUser, IGuidGenerator guidGenerator, IProductRepository productRepository, IUnitOfWork unitOfWork)
        {
            _orderRepository = orderRepository;
            _webDemoDbContext = webDemoDbContext;
            _objectMapper = objectMapper;
            _currentUser = currentUser;
            _guidGenerator = guidGenerator;
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
        }

        public OrderDto Create(CreateOrderDto createOrderDto)
        {
            Order order = new Order(_guidGenerator.Create());
            order = _objectMapper.Map(createOrderDto, order);

            if (_currentUser.Id != null)
            {
                //Claim[] claim = _currentUser.GetAllClaims();
                order.UserId = _currentUser.Id.Value;
            }
            _orderRepository.Create(order);

            //扣减库存
            foreach (var orderItems in order.OrderItems)
            {
                Product.Product product = _productRepository.GetProductById(orderItems.ProductId);
                product.ProductStock = product.ProductStock - orderItems.ItemCount;
                _productRepository.Update(product);
            }

            //abp事务，最终执行保存到数据库
            //await _unitOfWork.SaveChangesAsync();

            return _objectMapper.Map<Order, OrderDto>(order);
        }
        public void Update(UpdateOrderDto updateOrderDto)
        {
            var orders = _webDemoDbContext.Orders.Where(x => x.Id == updateOrderDto.Id).FirstOrDefault();
            Order order = _objectMapper.Map(updateOrderDto, orders);
            _orderRepository.Update(order);
        }

        public void Delete(Guid id)
        {
            Order order = new Order(id);
            var orders = _webDemoDbContext.Orders.AsNoTracking().Where(x => x.Id == id).FirstOrDefault();
            order.ConcurrencyStamp = orders.ConcurrencyStamp;
            _orderRepository.Delete(order);
        }

        public OrderDto GetOrderInfoById(Guid id)
        {
            Order orderInfo = _orderRepository.GetOrderInfoById(id);
            OrderDto orderInfoDto = _objectMapper.Map<Order, OrderDto>(orderInfo);
            return orderInfoDto;
        }

        public async Task<PagedResultDto<OrderDto>> GetOrderPagedInputDto(OrderPagedInputDto orderPagedInputDto)
        {
            var query = _webDemoDbContext.Orders.AsNoTracking()
               .Include(x => x.OrderItems.Where(x => x.ItemPrice > orderPagedInputDto.ItemPrice));
            List<Order> list = await query.PageBy(orderPagedInputDto).ToListAsync();
            int Count = query.Count();
            List<OrderDto> productDtos = _objectMapper.Map<List<Order>, List<OrderDto>>(list);
            return new PagedResultDto<OrderDto>(Count, productDtos);
        }

        public List<OrderDto> GetOrders()
        {
            // 1、数据库查询数据
            List<Order> orderList = _orderRepository.GetOrders();
            List<OrderDto> orderListDto = _objectMapper.Map<List<Order>, List<OrderDto>>(orderList);
            return orderListDto;
        }

        public bool OrderExists(Guid id)
        {
            return _orderRepository.OrderExists(id);
        }


    }
}
