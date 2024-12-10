namespace OnlineShopping.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }

        // Add this property to resolve the issue
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    }

}
