using Microsoft.AspNetCore.Mvc;
using VpApi.DTOs;
using VpBusinessLogic.Services;
using VpDataAccess.Models;

namespace VpApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly ICustomerService _customerService;

        public OrderController(IOrderService orderService, ICustomerService customerService)
        {
            _orderService = orderService;
            _customerService = customerService;
        }

        [HttpPost]
        public IActionResult CreateOrder([FromBody] CreateOrderDTO createOrderDTO)
        {
            try
            {
                if (createOrderDTO == null)
                {
                    return BadRequest("Order data is required.");
                }

                if (createOrderDTO.CustomerId == 0)
                {
                    if (string.IsNullOrEmpty(createOrderDTO.CustomerFirstName) || string.IsNullOrEmpty(createOrderDTO.CustomerLastName))
                    {
                        return BadRequest("Customer first and last name are required for guest checkout.");
                    }
                    var guestCustomer = new Customer {
                        FirstName = createOrderDTO.CustomerFirstName,
                        LastName = createOrderDTO.CustomerLastName
                    };
                    guestCustomer = _customerService.AddCustomer(guestCustomer);
                    createOrderDTO.CustomerId = guestCustomer.Id;
                }

                if (createOrderDTO.OrderItems == null || createOrderDTO.OrderItems.Count == 0)
                {
                    return BadRequest("At least one order item is required.");
                }

                var newOrder = new Order
                {
                    CreatedAt = DateTime.Now,
                    CustomerID = createOrderDTO.CustomerId,
                    OrderItems = createOrderDTO.OrderItems.Select(oi => new OrderItem
                    {
                        ProductId = oi.ProductId,
                        Quantity = oi.Quantity,
                        Price = oi.Price
                    }).ToList()
                };

                _orderService.AddOrder(newOrder);
                return Ok("Order successfully created.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetAnOrder(int id)
        {
            try
            {
                var order = _orderService.GetOrderById(id);
                if (order == null)
                {
                    return NotFound($"Order with id {id} not found.");
                }

                var orderDto = new OrderDTO
                {
                    Id = order.Id,
                    Customer = new CustomerDTO { Id = order.Customer.Id, FirstName = order.Customer.FirstName, LastName = order.Customer.LastName },
                    CreatedAt = order.CreatedAt,
                    OrderItems = new List<OrderItemDTO>()
                };

                foreach (var item in order.OrderItems)
                {
                    orderDto.OrderItems.Add(new OrderItemDTO
                    {
                        ProductId = item.Product.Id,
                        Quantity = item.Quantity,
                        Price = item.Price
                    });
                }

                return Ok(orderDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet]
        public IActionResult GetAllOrders()
        {
            try
            {
                var orders = _orderService.GetAllOrders();
                if (orders == null || !orders.Any())
                {
                    return NotFound("No orders found.");
                }

                var orderDtos = new List<OrderDTO>();
                foreach (var order in orders)
                {
                    var orderDto = new OrderDTO
                    {
                        Id = order.Id,
                        Customer = new CustomerDTO { Id = order.Customer.Id, FirstName = order.Customer.FirstName, LastName = order.Customer.LastName },
                        CreatedAt = order.CreatedAt,
                        OrderItems = new List<OrderItemDTO>()
                    };

                    foreach (var item in order.OrderItems)
                    {
                        orderDto.OrderItems.Add(new OrderItemDTO
                        {
                            ProductId = item.Product.Id,
                            Quantity = item.Quantity,
                            Price = item.Price
                        });
                    }

                    orderDtos.Add(orderDto);
                }

                return Ok(orderDtos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
