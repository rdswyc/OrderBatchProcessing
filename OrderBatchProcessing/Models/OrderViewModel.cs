namespace OrderBatchProcessing.Models
{
    public class OrderViewModel
    {
        public string OrderId { get; set; }
        public decimal Value { get; set; }

        public static explicit operator OrderViewModel(Order o) => new OrderViewModel
        {
            OrderId = $"O{o.Id}",
            Value = o.Value
        };
    }
}
