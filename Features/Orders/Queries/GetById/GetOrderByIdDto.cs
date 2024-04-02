namespace Store.Features.Orders.Queries.GetById
{
    public class GetOrderByIdDto
    {
        public int Id { get; set; }
        public DateTime RequestDate { get; set; }
        public DateTime? CloseDate { get; set; }
        public OrderStatus Status { get; set; }
        public int CustomerId { get; set; }
        public decimal TotalPrice { get; set; }
        public string CurrencyCode { get; set; }
        public decimal ExchangeRate { get; set; }
        public decimal ForeignPrice { get; set; }
        public bool IsClosed { get; set; }
        public ICollection<GetOrderDetailDto> OrderDetails { get; set; }
    }
}
