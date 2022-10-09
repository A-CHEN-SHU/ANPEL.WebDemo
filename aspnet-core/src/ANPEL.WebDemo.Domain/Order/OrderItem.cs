using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;

namespace ANPEL.WebDemo.Order
{
    public class OrderItem/* : Entity<Guid>*/
    {
        //[Key]
        public Guid Id { set; get; } // 主键
        public Guid OrderId { set; get; } // 订单编号
        public string OrderSn { set; get; } // 订单号
        public int ProductId { set; get; } // 商品编号
        public string ProductUrl { set; get; } // 商品主图
        public string ProductName { set; get; }// 商品名称
        public decimal ItemPrice { set; get; }  // 订单项单价
        public int ItemCount { set; get; } // 订单项数量
        public decimal ItemTotalPrice { set; get; } // 订单项总价
        public OrderItem()
        {
        }

        public OrderItem(Guid id) /*: base(id)*/
        {

        }
    }
}
