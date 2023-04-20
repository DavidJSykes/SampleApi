namespace VpApi.DTOs
{
    public class CreateOrderDTO
    {
        public DateTime OrderDate { get; set; }
        public int CustomerId { get; set; }
        public string CustomerFirstName { get; set; }
        public string CustomerLastName { get; set; }
        public List<CreateOrderItemDTO> OrderItems { get; set; }
    }
}
