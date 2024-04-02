namespace Store.Features.Orders.Post
{
    public class PostOrderCommand : IRequest<ResponseDto>
    {
        public List<OrderDetailDto> OrderDetails { get; set; }
        public bool HaveVoucher { get; set; }
        public string? DiscountPromoCode { get; set; }
        public string CurrencyCode { get; set; }
    }
}
