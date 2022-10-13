using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.Domain.Repositories;

namespace ANPEL.WebDemo.Product
{
    public class Product : /*FullAuditedAggregateRoot<Guid>:*/ AggregateRoot<Guid>
    {
        //public  Guid Id { set; get; } // 商品主键
        public string ProductCode { set; get; }    //商品编码
        public string ProductUrl { set; get; }         // 商品主图
        public string ProductTitle { set; get; }       //商品标题
        public string ProductDescription { set; get; }     // 图文描述
        public decimal ProductVirtualprice { set; get; } //商品虚拟价格
        public decimal ProductPrice { set; get; }       //价格
        public int ProductSort { set; get; }    //商品序号
        public int ProductSold { set; get; }        //已售件数
        public int ProductStock { set; get; }       //商品库存
        public string ProductStatus { set; get; } // 商品状态

        public virtual ICollection<ProductImage> ProductImages { get; set; }

        public Product()
        {
            ProductImages = new Collection<ProductImage>();
        }

        public Product(Guid id) : base(id)
        {
            ProductImages = new Collection<ProductImage>();
        }

        public Product(string ProductTitle)
        {
            // 业务规则：ProductTitle名称不能为空
            if (ProductTitle == null)
            {
                throw new Exception("商品名称不能为空");
            }
            ProductImages = new Collection<ProductImage>();
        }

        /// <summary>
        /// 添加商品图片
        /// </summary>
        public void AddProductImage(Guid ImageId, string ImageUrl,string ImageStatus)
        {
            // 1、创建一个商品图片
            ProductImage productImage = new ProductImage(ImageId);
            productImage.ImageUrl = ImageUrl;
            productImage.ImageStatus = ImageStatus;

            // 2、添加到集合中
            ProductImages.Add(productImage);
        }

        /// <summary>
        /// 移除商品图片
        /// </summary>
        public void RemoveProductImage(Guid ImageId)
        {
            // 1、判断guid ,然后删除指定商品
            ProductImage productImage= ProductImages.Where(x => x.Id == ImageId).FirstOrDefault();
            if (productImage != null)
            {
                ProductImages.Remove(productImage);
            }
        }
    }
}
