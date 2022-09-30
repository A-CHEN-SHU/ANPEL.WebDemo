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
            CreateMap<Product.Product, ProductDto>();
            CreateMap<PagedAndSortedResultRequestDto, Product.Product>();
            CreateMap<CreateProductDto, Product.Product>();
            CreateMap<UpdateProductDto, Product.Product>();
            CreateMap<ProductImage, ProductImageDto>();
        }
    }
}
