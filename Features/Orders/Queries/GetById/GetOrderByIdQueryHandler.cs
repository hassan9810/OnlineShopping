namespace Store.Features.Orders.Queries.GetById
{
    public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, ResponseDto>
    {
        private readonly IGRepository<Order> _orderRepo;
        private readonly ResponseHelper _responseHelper;
        private readonly IMapper _mapper;

        public GetOrderByIdQueryHandler(IGRepository<Order> orderRepo, IMapper mapper)
        {
            _orderRepo = orderRepo;
            _responseHelper = new ResponseHelper();
            _mapper = mapper;
        }

        public async Task<ResponseDto> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
        {
            var order = _orderRepo.GetAll(order => order.Id == request.OrderId)
                .Include(x => x.OrderDetails).FirstOrDefault();

            if (order == null)
            {
                return _responseHelper.NotFound("Order not found");
            }

            var orderDto = _mapper.Map<GetOrderByIdDto>(order);
            return _responseHelper.RetrievedSuccessfully(orderDto, "Order retrieved successfully");
        }
    }
}
