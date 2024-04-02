namespace Store.Features.Orders.Commands.Put
{
    public class PutOrderIsClosedHandler : IRequestHandler<PutOrderIsClosedCommand, ResponseDto>
    {
        private readonly IGRepository<Order> _orderRepository;
        private readonly ResponseHelper _response;

        public PutOrderIsClosedHandler(IGRepository<Order> orderRepository, ResponseHelper response)
        {
            _orderRepository = orderRepository;
            _response = response;
        }

        public async Task<ResponseDto> Handle(PutOrderIsClosedCommand request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetFirstAsync(order => order.Id == request.OrderId);
            if (order == null)
                return _response.NotFound("Order not found");

            order.IsClosed = request.IsClosed;
            order.CloseDate = request.IsClosed ? DateTime.Now : (DateTime?)null;

            order.Status = request.IsClosed ? OrderStatus.Closed : OrderStatus.Open;

            _orderRepository.Update(order);
            _orderRepository.Save();

            return _response.SavedSuccessfully(order.IsClosed ? "Order closed successfully" : "Order reopened successfully");
        }
    }
}
