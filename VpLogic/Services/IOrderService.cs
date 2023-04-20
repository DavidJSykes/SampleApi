using System.Collections.Generic;
using VpDataAccess.Models;

namespace VpBusinessLogic.Services
{
    public interface IOrderService
    {
        IEnumerable<Order> GetAllOrders();
        Order GetOrderById(int id);
        Order AddOrder(Order order);
        Order UpdateOrder(Order order);
        void DeleteOrder(int id);
    }
}
