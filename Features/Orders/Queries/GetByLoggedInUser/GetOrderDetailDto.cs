namespace Store.Features.Orders.Queries.GetByLoggedInUser
{
    public class GetOrderDetailDto
    {
        public int OrderId { get; set; }
        public int ItemId { get; set; }
        public decimal ItemPrice { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
