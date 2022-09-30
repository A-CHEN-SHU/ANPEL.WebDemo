using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ANPEL.WebDemo.Product
{
    public class ProductManager
    {
        private readonly IProductRepository _productRepository;

        public ProductManager(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async void CreateAsync(string ProductTitle)
        {
            /*IEnumerable<Product> products = _productRepository.GetProductByName(ProductTitle);
            if (products != null)
            {
                throw new Exception("商品名称不能重复");
            }*/
        }
    }
}
