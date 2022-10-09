using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace ANPEL.WebDemo.Order
{
    public interface IOrderAppService
    {
        List<OrderDto> GetOrders();
        OrderDto GetOrderInfoById(Guid id);
        void Create(CreateOrderDto createOrder);
        void Update(UpdateOrderDto updateOrder);
        void Delete(Guid id);
        bool OrderExists(Guid id);
        Task<PagedResultDto<OrderDto>> GetOrderPagedInputDto(OrderPagedInputDto orderPagedInputDto);
    }
}
