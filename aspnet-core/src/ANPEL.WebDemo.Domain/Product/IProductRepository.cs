using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ANPEL.WebDemo.Product
{
    /// <summary>
    /// 定义商品仓储
    /// </summary>
    public interface IProductRepository
    {
        IEnumerable<Product> GetProducts();
        Product GetProductById(Guid id);
        Product GetProductIncludeImageByProductId(Guid id);

        IEnumerable<Product> GetProductByName(string ProductName);
        void Create(Product Product);
        void Update(Product Product);
        void Delete(Product Product);
        bool ProductExists(Guid id);
        //IQueryable<Product> GetPageProductList(Product product);
    }
}
