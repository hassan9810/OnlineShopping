namespace Store.Features.Orders.Commands.Put
{
    public class PutOrderIsClosedCommand : IRequest<ResponseDto>
    {
        public int OrderId { get; set; }
        public bool IsClosed { get; set; }

        public PutOrderIsClosedCommand(int orderId, bool isClosed)
        {
            OrderId = orderId;
            IsClosed = isClosed;
        }
    }
}
