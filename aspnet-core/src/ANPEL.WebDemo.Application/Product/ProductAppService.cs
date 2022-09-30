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
using Volo.Abp.Uow;

namespace ANPEL.WebDemo.Product
{
    /// <summary>
    /// 商品服务实现
    /// </summary>
    [Dependency(ServiceLifetime.Transient)]
    public class ProductAppService : IProductAppService
    {
        public IProductRepository _productRepository; // 商品仓储

        //public ProductManager _ProductManager; // 商品领域服务
        public IGuidGenerator GuidGenerator { get; set; } // Guid生成器

        //public IObjectMapper ObjectMapper { get; set; }

        private WebDemoDbContext _webDemoDbContext;

        public IUnitOfWorkManager _unitOfWorkManager;//

        public ProductAppService(IProductRepository productRepository, IGuidGenerator guidGenerator/*, IObjectMapper objectMapper*/, WebDemoDbContext webDemoDbContext, IUnitOfWorkManager unitOfWorkManager)
        {
            _productRepository = productRepository;
            ////_ProductManager = productManager;
            GuidGenerator = guidGenerator;
            //ObjectMapper = objectMapper;
            _webDemoDbContext = webDemoDbContext;
            _unitOfWorkManager = unitOfWorkManager;
        }
        /// <summary>
        /// 查询所有商品
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ProductDto> GetProducts()
        {
            // 1、数据库查询数据
            IEnumerable<Product> products = _productRepository.GetProducts();
            // 2、AutoMapper自动映射实体
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Product, ProductDto>();
                cfg.CreateMap<ProductImage, ProductImageDto>();
            });

            IMapper mapper = configuration.CreateMapper();

            List<ProductDto> productDtos = mapper.Map<IEnumerable<Product>, List<ProductDto>>(products);
            return productDtos;
        }
        /// <summary>
        /// 根据商品ID查询商品
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ProductDto GetProductById(Guid id)
        {
            // 1、数据库查询数据
            Product products = _productRepository.GetProductById(id);
            // 2、AutoMapper自动映射实体
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Product, ProductDto>();
            });

            IMapper mapper = configuration.CreateMapper();

            ProductDto productDtos = mapper.Map<Product, ProductDto>(products);
            return productDtos;
        }
        /// <summary>
        /// 创建商品
        /// </summary>
        /// <param name="createProductDto"></param>
        public void Create(CreateProductDto createProductDto)
        {
            // 1、AutoMapper自动映射实体
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CreateProductDto, Product>();
                cfg.CreateMap<ProductImageCreateDto, ProductImage>();
            });

            IMapper mapper = configuration.CreateMapper();

            Product product = new Product(GuidGenerator.Create());
            product = mapper.Map(createProductDto, product);

            // 1、先查询商品
            Product product1 = _productRepository.GetProductByName(product.ProductTitle).FirstOrDefault();
            if (product1 != null)
            {
                throw new Exception("商品名称不能重复");
            }
            // 1、规则判断
            // _ProductManager.CreateAsync(createProductDto.ProductTitle);

            // 2、创建商品
            _productRepository.Create(product);
        }
        /// <summary>
        /// 更新商品
        /// </summary>
        /// <param name="updateProductDto"></param>
        public void Update(UpdateProductDto updateProductDto)
        {
            Product product = new Product();

            // 1、AutoMapper自动映射实体
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UpdateProductDto, Product>();
            });

            IMapper mapper = configuration.CreateMapper();
            product = mapper.Map(updateProductDto, product);
            _productRepository.Update(product);
        }

        /// <summary>
        /// 根据商品ID删除商品
        /// </summary>
        /// <param name="id"></param>
        public void Delete(Guid id)
        {
            Product product = new Product { Id = id };
            _productRepository.Delete(product);
        }
        /// <summary>
        /// 判断商品是否存在
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool ProductExists(Guid id)
        {
            return _productRepository.ProductExists(id);
        }
        /// <summary>
        /// 创建商品图片
        /// </summary>
        /// <param name="ProductId"></param>
        /// <param name="productImageCreateDto"></param>
        public void CreateProductImage(Guid ProductId, ProductImageCreateDto productImageCreateDto)
        {
            // 1、先查询商品
            Product product = _productRepository.GetProductById(ProductId);
            if (product == null)
            {
                throw new NotImplementedException("商品不存在");
            }
            // 2、添加商品图片
            product.AddProductImage(GuidGenerator.Create(), productImageCreateDto.ImageUrl, productImageCreateDto.ImageStatus);

            // 3、更新商品聚合根对象
            _productRepository.Update(product);
        }
        /// <summary>
        /// 删除商品图片
        /// </summary>
        /// <param name="ProductId"></param>
        /// <param name="deleteProductImageDto"></param>
        public void DeleteProductImage(Guid ProductId, DeleteProductImageDto deleteProductImageDto)
        {
            // 1、先查询商品
            Product product = _productRepository.GetProductIncludeImageByProductId(ProductId);
            if (product == null)
            {
                throw new NotImplementedException("商品不存在");
            }

            // 2、添加商品图片
            product.RemoveProductImage(deleteProductImageDto.ProductImageId);

            // 3、更新商品聚合根对象
            _productRepository.Update(product);
        }
        /// <summary>
        /// 根据商品名称查询商品
        /// </summary>
        public void GetProductByName()
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 根据分页查询商品数据
        /// </summary>
        /// <param name="productPagedInputDto"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<ProductDto>> GetPageProductList(ProductPagedInputDto productPagedInputDto)
        {
            var query = _webDemoDbContext.Products.AsNoTracking()
                .WhereIf(productPagedInputDto.ProductPrice > 0, x => x.ProductPrice > productPagedInputDto.ProductPrice)
                .Include(x => x.ProductImages);
            List<Product> list = await query.PageBy(productPagedInputDto).ToListAsync();
            int tasksCount = query.Count();
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Product, ProductDto>();
                cfg.CreateMap<ProductImage, ProductImageDto>();
            });
            IMapper mapper = configuration.CreateMapper();
            List<ProductDto> productDtos = mapper.Map<IEnumerable<Product>, List<ProductDto>>(list);
            return new PagedResultDto<ProductDto>(tasksCount, productDtos);
        }
    }
}
