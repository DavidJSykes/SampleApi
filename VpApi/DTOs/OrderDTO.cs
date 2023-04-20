using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace VpApi.DTOs
{
    public class OrderDTO
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public CustomerDTO Customer { get; set; }
        public List<OrderItemDTO> OrderItems { get; set; }
    }
}
