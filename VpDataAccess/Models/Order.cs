using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VpDataAccess.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        public int CustomerID { get; set; }
        public decimal Price { get; set; }
        public DateTime CreatedAt { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual List<OrderItem> OrderItems { get; set; }
    }
}
