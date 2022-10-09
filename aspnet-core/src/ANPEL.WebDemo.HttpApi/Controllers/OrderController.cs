using ANPEL.WebDemo.Order;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc;

namespace ANPEL.WebDemo.Controllers
{
    /// <summary>
    /// 商品服务控制器
    /// </summary>
    [Route("Ordes")]
    [ApiController]
    //[Authorize]
    public class OrderController : AbpController
    {
        public IOrderAppService _orderAppService;

        public OrderController(IOrderAppService orderAppService)
        {
            _orderAppService = orderAppService;
        }
        [HttpGet]
        public ActionResult<List<OrderDto>> GetProducts()
        {
            //1、自己写的查询
            return _orderAppService.GetOrders();
        }
        [HttpGet("{id}")]
        public ActionResult<OrderDto> GetOrderInfoById(Guid id)
        {
            var orderDto = _orderAppService.GetOrderInfoById(id);

            if (orderDto == null)
            {
                return NotFound();
            }

            return orderDto;
        }

        [HttpDelete("{id}")]
        public bool DeleteOrder(Guid id)
        {
            var order = _orderAppService.OrderExists(id);
            if (!order)
            {
                return order;
            }
            _orderAppService.Delete(id);
            return order;
        }
        [HttpPost]
        public ActionResult<OrderDto> CreateProduct(CreateOrderDto createOrderDto)
        {
            _orderAppService.Create(createOrderDto);
            return CreatedAtAction("GetOrder", createOrderDto);
        }
        [HttpGet]
        private bool OrderExists(Guid id)
        {
            return _orderAppService.OrderExists(id);
        }
        // PUT: api/Products/5
        [HttpPut]
        public IActionResult PutProduct(UpdateOrderDto updateOrderDto)
        {
            var order = _orderAppService.OrderExists(updateOrderDto.Id);
            if (!order)
            {
                throw new Exception("商品不存在");
            }
            _orderAppService.Update(updateOrderDto);

            return NoContent();
        }

        [HttpGet("GetOrderPagedInputDto")]
        public Task<PagedResultDto<OrderDto>> GetOrderPagedInputDto([FromQuery] OrderPagedInputDto orderPagedInputDto)
        {
            return _orderAppService.GetOrderPagedInputDto(orderPagedInputDto);
        }
    }
}
