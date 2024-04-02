namespace Store.Features.Orders.Queries.GetById
{
    public class GetOrderByIdQuery : IRequest<ResponseDto>
    {
        public int OrderId { get; set; }

        public GetOrderByIdQuery(int orderId)
        {
            OrderId = orderId;
        }
    }
}
