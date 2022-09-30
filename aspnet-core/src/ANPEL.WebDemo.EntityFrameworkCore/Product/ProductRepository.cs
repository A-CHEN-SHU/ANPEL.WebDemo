using ANPEL.WebDemo.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace ANPEL.WebDemo.Product
{
    /// <summary>
    /// 商品仓储实现
    /// </summary>
    [Dependency(ServiceLifetime.Transient)]
    /// 
    public class ProductRepository : IProductRepository
    {
        /// <summary>
        /// 数据库上下文
        /// </summary>
        public WebDemoDbContext _webDemoDbContext;

        public ProductRepository(WebDemoDbContext webDemoDbContext)
        {
            _webDemoDbContext = webDemoDbContext;
        }
        /// <summary>
        /// 创建商品
        /// </summary>
        /// <param name="Product"></param>
        public void Create(Product Product)
        {
            ////默认
            {
                _webDemoDbContext.Products.Add(Product);
                _webDemoDbContext.SaveChanges();
            }
            ////没有异常 commit提交两条数据
            //{
            //    using (var transaction = _webDemoDbContext.Database.BeginTransaction())
            //    {
            //        try
            //        {
            //            ICollection<ProductImage> ProductImages = new Collection<ProductImage>();
            //            ProductImages.Add(new ProductImage { Id = Guid.Parse("08daa1ef-a571-4c93-86a0-88c8071dad31"), ProductId = Guid.Parse("08daa1ef-a571-4c93-86a0-88c8071dad30"), ImageSort = 1, ImageStatus = "1", ImageUrl = "1" });
            //            _webDemoDbContext.Products.Add(Product);
            //            _webDemoDbContext.SaveChanges();
            //            Product pp = new Product
            //            {
            //                Id = Guid.Parse("08daa1ef-a571-4c93-86a0-88c8071dad30"),
            //                ProductSold = 1,
            //                ProductCode = "1",
            //                ProductDescription = "1",
            //                ProductImages = ProductImages,
            //                ProductPrice = 1,
            //                ProductSort = 1,
            //                ProductStatus = "1",
            //                ProductStock = 1,
            //                ProductTitle = "1",
            //                ProductUrl = "1",
            //                ProductVirtualprice = 1
            //            };
            //            _webDemoDbContext.Products.Add(pp);
            //            _webDemoDbContext.SaveChanges();
            //            //只有提交事务才会添加到数据库
            //            transaction.Commit();
            //        }
            //        catch (Exception ex)
            //        {
            //transaction.Rollback();
            //        }
            //    };
            //}
            ////发生异常只提交一条数据
            //{
            //    using (var transaction = _webDemoDbContext.Database.BeginTransaction())
            //    {
            //        try
            //        {
            //            ICollection<ProductImage> ProductImages = new Collection<ProductImage>();
            //            ProductImages.Add(new ProductImage { Id = Guid.Parse("08daa1ef-a571-4c93-86a0-88c8071dad31"), ProductId = Guid.Parse("08daa1ef-a571-4c93-86a0-88c8071dad30"), ImageSort = 2, ImageStatus = "2", ImageUrl = "2" });
            //            _webDemoDbContext.Products.Add(Product);
            //            _webDemoDbContext.SaveChanges();
            //            //创建保存点，并在失败时回滚到该保存点：
            //            transaction.CreateSavepoint("savePoint1");
            //            Product pp = new Product
            //            {
            //                Id = Guid.Parse("08daa1ef-a571-4c93-86a0-88c8071dad30"),
            //                ProductSold = 2,
            //                ProductCode = "2",
            //                ProductDescription = "2",
            //                ProductImages = ProductImages,
            //                ProductPrice = 2,
            //                ProductSort = 2,
            //                ProductStatus = "2",
            //                ProductStock = 2,
            //                ProductTitle = "2",
            //                ProductUrl = "2",
            //                ProductVirtualprice = 2
            //            };
            //            _webDemoDbContext.Products.Add(pp);
            //            _webDemoDbContext.SaveChanges();
            //            throw new Exception();
            //            //只有提交事务才会添加到数据库
            //        }
            //        catch (Exception ex)
            //        {
            //            transaction.RollbackToSavepoint("savePoint1");
            //            //异常进行回滚
            //        }
            //        finally
            //        {
            //            transaction.Commit();
            //        }
            //    };
            //}
            ////发生异常没有创建保存点，不会有数据入库
            //{
            //    using (var transaction = _webDemoDbContext.Database.BeginTransaction())
            //    {
            //        try
            //        {
            //            ICollection<ProductImage> ProductImages = new Collection<ProductImage>();
            //            ProductImages.Add(new ProductImage { Id = Guid.Parse("08daa1ef-a571-4c93-86a0-88c8071dad31"), ProductId = Guid.Parse("08daa1ef-a571-4c93-86a0-88c8071dad30"), ImageSort = 2, ImageStatus = "2", ImageUrl = "2" });
            //            _webDemoDbContext.Products.Add(Product);
            //            _webDemoDbContext.SaveChanges();
            //            Product pp = new Product
            //            {
            //                Id = Guid.Parse("08daa1ef-a571-4c93-86a0-88c8071dad30"),
            //                ProductSold = 2,
            //                ProductCode = "2",
            //                ProductDescription = "2",
            //                ProductImages = ProductImages,
            //                ProductPrice = 2,
            //                ProductSort = 2,
            //                ProductStatus = "2",
            //                ProductStock = 2,
            //                ProductTitle = "2",
            //                ProductUrl = "2",
            //                ProductVirtualprice = 2
            //            };
            //            _webDemoDbContext.Products.Add(pp);
            //            _webDemoDbContext.SaveChanges();
            //            throw new Exception();
            //            //只有提交事务才会添加到数据库
            //            transaction.Commit();
            //        }
            //        catch (Exception ex)
            //        {
            //            //异常进行回滚
            //            transaction.Rollback();
            //        }
            //    };
            //}
        }
        /// <summary>
        /// 删除商品
        /// </summary>
        /// <param name="Product"></param>
        public void Delete(Product Product)
        {
            _webDemoDbContext.Remove(Product);
            _webDemoDbContext.SaveChanges();
        }
        /// <summary>
        /// 根据商品ID查询商品
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Product GetProductById(Guid id)
        {
            return _webDemoDbContext.Products.AsNoTracking().Where(x => x.Id == id).FirstOrDefault();
        }
        /// <summary>
        /// 根据商品id关联查询商品下的图片
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Product GetProductIncludeImageByProductId(Guid id)
        {
            return _webDemoDbContext.Products.Include(x => x.ProductImages).Where(x => x.Id == id).FirstOrDefault();
        }
        /// <summary>
        /// 查询所有，商品关联图片
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Product> GetProducts()
        {
            //Include关联表方法
            return _webDemoDbContext.Products.Include(product => product.ProductImages).ToList();
        }
        /// <summary>
        /// 更新商品
        /// </summary>
        /// <param name="Product"></param>
        public void Update(Product Product)
        {
            _webDemoDbContext.Products.Update(Product);
            _webDemoDbContext.SaveChanges();
        }
        /// <summary>
        /// 根据商品ID判断是否存在
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool ProductExists(Guid id)
        {
            return _webDemoDbContext.Products.Any(e => e.Id == id);
        }
        /// <summary>
        /// 根据商品名称查询商品
        /// </summary>
        /// <param name="ProductName"></param>
        /// <returns></returns>
        public IEnumerable<Product> GetProductByName(string ProductName)
        {
            return _webDemoDbContext.Products.Where(e => e.ProductTitle == ProductName);
        }
        ///// <summary>
        ///// 分页查询商品
        ///// </summary>
        ///// <returns></returns>
        //public IQueryable<Product> GetPageProductList(decimal ProductPrice,ref int count)
        //{
        //    var query = _webDemoDbContext.Products.WhereIf(ProductPrice>0, x=>x.ProductPrice>ProductPrice).Include(x => x.ProductImages);
        //    IQueryable<Product> list = (IQueryable<Product>) query.PageBy(productPagedInputDto).ToListAsync();
        //    //Include关联表方法
        //    return list;
        //}

    }
}
