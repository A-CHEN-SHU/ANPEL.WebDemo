using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;

namespace ANPEL.WebDemo.Order
{
    public class Order : AggregateRoot<Guid>
    {
        //[Key]
        //public int Id { set; get; } // 主键
        public string OrderType { set; get; } // 订单类型
                                              // public string OrderFlag { set; get; } // 订单标志
        public Guid UserId { set; get; } // 用户Id
        public string OrderSn { set; get; }// 订单号
        public string OrderTotalPrice { set; get; } // 订单总价
        public DateTime Createtime { set; get; } // 创建时间
        public DateTime Updatetime { set; get; } // 更新时间
        public DateTime Paytime { set; get; }// 支付时间
        public DateTime Sendtime { set; get; }// 发货时间
        public DateTime Successtime { set; get; }// 订单完成时间
        public int OrderStatus { set; get; } // 订单状态
        public string OrderName { set; get; } // 订单名称
        public string OrderTel { set; get; } // 订单电话
        public string OrderAddress { set; get; } // 订单地址
        public string OrderRemark { set; get; }// 订单备注

        // 订单项
        public virtual ICollection<OrderItem> OrderItems { get; set; }

        public Order()
        {
            OrderItems = new Collection<OrderItem>();
        }

        public Order(Guid id) : base(id)
        {
            OrderItems = new Collection<OrderItem>();
        }
        /// <summary>
        /// 添加订单项
        /// </summary>
        /// <param name="itemId"></param>
        /// <param name="OrderSn"></param>
        /// <param name="ProductName"></param>
        /// <param name="ItemPrice"></param>
        public void AddOrderItem(Guid itemId, string OrderSn, string ProductName, decimal ItemPrice)
        {
            //添加订单项
            OrderItem orderItem = new OrderItem(itemId);
            orderItem.OrderSn = OrderSn;
            orderItem.ProductName = ProductName;
            orderItem.ItemPrice = ItemPrice;
            OrderItems.Add(orderItem);
        }

        /// <summary>
        /// 删除订单项
        /// </summary>
        public void RemoveOrderImage(Guid orderId)
        {
            // 1、判断guid ,然后删除订单项
            OrderItem orderItems = OrderItems.Where(x => x.Id == orderId).FirstOrDefault();
            if (orderItems != null)
            {
                OrderItems.Remove(orderItems);
            }
        }
    }
}
