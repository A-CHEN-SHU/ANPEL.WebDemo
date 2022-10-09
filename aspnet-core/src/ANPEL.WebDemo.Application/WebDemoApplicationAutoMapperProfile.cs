using ANPEL.WebDemo.Product;
using AutoMapper;
using Volo.Abp.Application.Dtos;

namespace ANPEL.WebDemo
{
    public class WebDemoApplicationAutoMapperProfile : Profile
    {
        public WebDemoApplicationAutoMapperProfile()
        {
            /* You can configure your AutoMapper mapping configuration here.
             * Alternatively, you can split your mapping configurations
             * into multiple profile classes for a better organization. */

            /*Product实体映射*/
            CreateMap<Product.Product, ProductDto>();
            CreateMap<PagedAndSortedResultRequestDto, Product.Product>();
            CreateMap<CreateProductDto, Product.Product>();
            CreateMap<UpdateProductDto, Product.Product>();
            CreateMap<ProductImage, ProductImageDto>();

            /*Order实体映射*/
            CreateMap<Order.Order,Order.OrderDto >();
            CreateMap<Order.OrderDto, Order.Order>();
            CreateMap<Order.CreateOrderDto, Order.Order>();
            CreateMap<Order.CreateOrderItemDto, Order.OrderItem>();
            CreateMap<Order.UpdateOrderDto, Order.Order>();
            CreateMap<Order.OrderItemDto, Order.OrderItem>();
            CreateMap<Order.OrderItem, Order.OrderItemDto>();
        }
    }
}
