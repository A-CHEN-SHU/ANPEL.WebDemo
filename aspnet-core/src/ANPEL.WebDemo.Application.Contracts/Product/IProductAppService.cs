using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace ANPEL.WebDemo.Product
{
    public interface IProductAppService
    {
        IEnumerable<ProductDto> GetProducts();
        ProductDto GetProductById(Guid id);
        void Create(CreateProductDto createProductDto);
        void Update(UpdateProductDto updateProductDto);
        void Delete(Guid id);
        bool ProductExists(Guid id);

        /// <summary>
        /// 添加商品图片
        /// </summary>
        /// <param name="productImageCreateDto"></param>
        public void CreateProductImage(Guid ProductId, ProductImageCreateDto productImageCreateDto);

        /// <summary>
        /// 删除指定商品下面的图片
        /// </summary>
        /// <param name="productImageCreateDto"></param>
        public void DeleteProductImage(Guid ProductId, DeleteProductImageDto deleteProductImageDto);

        public void GetProductByName();

        public Task<PagedResultDto<ProductDto>> GetPageProductList(ProductPagedInputDto productPagedInputDto);
    }
}
