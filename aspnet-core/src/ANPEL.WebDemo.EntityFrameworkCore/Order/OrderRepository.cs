using ANPEL.WebDemo.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;

namespace ANPEL.WebDemo.Order
{
    [Dependency(ServiceLifetime.Transient)]
    public class OrderRepository : IOrderRepository
    {
        public readonly WebDemoDbContext _webDemoDbContext;

        public OrderRepository(WebDemoDbContext webDemoDbContext)
        {
            _webDemoDbContext = webDemoDbContext;
        }

        //IRepository<Order> _orderRepository;

        //public OrderRepository(IRepository<Order> orderRepository)
        //{
        //    _orderRepository = orderRepository;
        //}

        public void Create(Order Order)
        {
            _webDemoDbContext.Orders.Add(Order);
            _webDemoDbContext.SaveChanges();
        }

        public void Delete(Order Order)
        {
            _webDemoDbContext.Remove(Order);
            _webDemoDbContext.SaveChanges();
        }

        public Order GetOrderInfoById(Guid id)
        {
            return _webDemoDbContext.Orders.AsNoTracking().Where(x => x.Id == id).FirstOrDefault();
        }
        public IQueryable GetOrderQueryableById(Guid id)
        {
            return _webDemoDbContext.Orders.AsNoTracking().Where(x => x.Id == id);
        }
        public List<Order> GetOrders()
        {
            return _webDemoDbContext.Orders.Include(x => x.OrderItems).ToList();
        }

        public bool OrderExists(Guid id)
        {
            return _webDemoDbContext.Orders.Any(x=>x.Id==id);
        }

        public void Update(Order Order)
        {
            _webDemoDbContext.Orders.Update(Order);
            _webDemoDbContext.SaveChanges();
        }
    }
}
