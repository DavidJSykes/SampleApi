using System.Collections.Generic;
using VpDataAccess.Models;
using VpDataAccess.Repositories;

namespace VpBusinessLogic.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IProductService _productService;

        public OrderService(IOrderRepository orderRepository, IProductService productService)
        {
            _orderRepository = orderRepository;
            _productService = productService;
        }

        public IEnumerable<Order> GetAllOrders()
        {
            return _orderRepository.GetAllOrders();
        }

        public Order GetOrderById(int id)
        {
            return _orderRepository.GetOrderById(id);
        }

        public Order AddOrder(Order order)
        {
            foreach (var orderItem in order.OrderItems)
            {
                var product = _productService.GetProductById(orderItem.ProductId);
                if (product == null)
                {
                    throw new ArgumentException($"No product with ID {orderItem.ProductId} found");
                }
                if (product.Price != orderItem.Price)
                {
                    throw new ArgumentException($"Product {orderItem.ProductId} should have a unit price of {product.Price} instead received {orderItem.Price}");
                }
            }
            return _orderRepository.AddOrder(order);
        }

        public Order UpdateOrder(Order order)
        {
            return _orderRepository.UpdateOrder(order);
        }

        public void DeleteOrder(int id)
        {
            _orderRepository.DeleteOrder(id);
        }
    }
}
