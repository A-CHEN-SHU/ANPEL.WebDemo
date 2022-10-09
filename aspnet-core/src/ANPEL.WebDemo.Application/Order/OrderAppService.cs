using ANPEL.WebDemo.EntityFrameworkCore;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Guids;
using Volo.Abp.Users;

namespace ANPEL.WebDemo.Order
{
    [Dependency(ServiceLifetime.Transient)]
    public class OrderAppService :/* WebDemoAppService,*/ IOrderAppService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly WebDemoDbContext _webDemoDbContext;
        private Volo.Abp.ObjectMapping.IObjectMapper _objectMapper;
        private ICurrentUser _currentUser;
        private IGuidGenerator _guidGenerator;

        public OrderAppService(IOrderRepository orderRepository, WebDemoDbContext webDemoDbContext, Volo.Abp.ObjectMapping.IObjectMapper objectMapper, ICurrentUser currentUser, IGuidGenerator guidGenerator)
        {
            _orderRepository = orderRepository;
            _webDemoDbContext = webDemoDbContext;
            _objectMapper = objectMapper;
            _currentUser = currentUser;
            _guidGenerator = guidGenerator;
        }

        public void Create(CreateOrderDto createOrderDto)
        {
            Order order = new Order(_guidGenerator.Create());
            order = _objectMapper.Map(createOrderDto, order);
            // 1、AutoMapper自动映射实体
            //var configuration = new MapperConfiguration(cfg =>
            //{
            //    cfg.CreateMap<CreateOrderDto, Order>();
            //    cfg.CreateMap<CreateOrderItemDto, OrderItem>();
            //});

            //IMapper mapper = configuration.CreateMapper();

            //Order order = new Order(_guidGenerator.Create());
            //order = mapper.Map(createOrderDto, order);
            order.UserId = _currentUser.Id.Value;
            _orderRepository.Create(order);
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
