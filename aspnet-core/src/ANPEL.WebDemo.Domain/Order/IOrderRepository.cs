using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace ANPEL.WebDemo.Order
{
    public interface IOrderRepository
    {
        List<Order> GetOrders();
        Order GetOrderInfoById(Guid id);
        void Create(Order Order);
        void Update(Order Order);
        void Delete(Order Order);
        bool OrderExists(Guid id);
    }
}
