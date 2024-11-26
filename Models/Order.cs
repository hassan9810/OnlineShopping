namespace Store.Models
{
    public class Order : BaseEntity
    {
        public DateTime RequestDate { get; set; }
        public DateTime? CloseDate { get; set; }
        public OrderStatus Status { get; set; }
        public int CustomerId { get; set; }
        public bool HaveVoucher { get; set; } = false;
        public string? DiscountPromoCode { get; set; }
        public decimal DiscountValue { get; set; }
        public decimal TotalPrice { get; set; }
        public string CurrencyCode { get; set; }
        public decimal ExchangeRate { get; set; }
        public decimal ForeignPrice { get; set; }
        public bool IsClosed { get; set; } = false;
        public User Customer { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; }
    }
}