using ANPEL.WebDemo.Product;
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
    [Route("Products")]
    [ApiController]
    //[Authorize]
    public class ProductController : AbpController
    {
        public IProductAppService _productAppService { set; get; }

        public ProductController(IProductAppService productAppService)
        {
            _productAppService = productAppService;
        }
        // GET: api/Products
        [HttpGet]
        public ActionResult<IEnumerable<ProductDto>> GetProducts()
        {
            //1、自己写的查询
            return _productAppService.GetProducts().ToList();
            //// 2、框架提供查询
            //return _productAppService.GetListAsync(new PagedAndSortedResultRequestDto()).Result.Items.ToList();
        }
        // GET: api/Products/5
        [HttpGet("{id}")]
        public ActionResult<ProductDto> GetProduct(Guid id)
        {
            var product = _productAppService.GetProductById(id);

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        [HttpPost]
        public ActionResult<ProductDto> CreateProduct(CreateProductDto createProductDto)
        {
            _productAppService.Create(createProductDto);
            return CreatedAtAction("GetProduct", createProductDto);
        }


        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public ActionResult<ProductDto> DeleteProduct(Guid id)
        {
            var product = _productAppService.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }
            _productAppService.Delete(id);
            return product;
        }


        [HttpPost("{ProductId}/AddProductImage")]
        public ActionResult<ProductDto> PostProductImage(Guid ProductId, ProductImageCreateDto productImageCreateDto)
        {
            _productAppService.CreateProductImage(ProductId, productImageCreateDto);
            return null;
        }
        [HttpPost("{ProductId}/DeleteProductImage")]
        public ActionResult<ProductDto> DeleteProductImage(Guid ProductId, DeleteProductImageDto deleteProductImageDto)
        {
            _productAppService.DeleteProductImage(ProductId, deleteProductImageDto);
            return null;
        }
        private bool ProductExists(Guid id)
        {
            return _productAppService.ProductExists(id);
        }
        // PUT: api/Products/5
        [HttpPut]
        public IActionResult PutProduct(UpdateProductDto updateProductDto)
        {
            var product = _productAppService.GetProductById(updateProductDto.id);
            if (product == null)
            {
                throw new Exception("商品不存在");
            }
            _productAppService.Update(updateProductDto);

            return NoContent();
        }
        [HttpGet("GetPageProductList")]
        public Task<PagedResultDto<ProductDto>> GetPageProductList([FromQuery] ProductPagedInputDto productPagedInputDto)
        {
          return _productAppService.GetPageProductList(productPagedInputDto);
        }
    }
}
