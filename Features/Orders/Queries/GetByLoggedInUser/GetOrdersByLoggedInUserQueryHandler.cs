namespace Store.Features.Orders.Queries.GetByLoggedInUser
{
    public class GetOrdersByLoggedInUserQueryHandler : IRequestHandler<GetOrdersByLoggedInUserQuery, ResponseDto>
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IGRepository<Order> _orderRepo;
        private readonly ResponseHelper _responseHelper;
        private readonly int loggedInUserId;
        private readonly IMapper _mapper;

        public GetOrdersByLoggedInUserQueryHandler(IHttpContextAccessor contextAccessor,IGRepository<Order> orderRepo, IMapper mapper)
        {
            _orderRepo = orderRepo;
            _contextAccessor = contextAccessor;
            loggedInUserId = LoggedInUserProvider.GetLoggedInUserId(_contextAccessor);
            _responseHelper = new ResponseHelper();
            _mapper = mapper;
        }

        public async Task<ResponseDto> Handle(GetOrdersByLoggedInUserQuery request, CancellationToken cancellationToken)
        {
            var orders = _orderRepo.GetAll(o => o.CustomerId == loggedInUserId
                    && !o.IsClosed && o.Status != OrderStatus.Closed)
                    .Include(ord => ord.OrderDetails)
                    .Select(o => new OrderDto
                    {
                        Id = o.Id,
                        RequestDate = o.RequestDate,
                        CloseDate = o.CloseDate,
                        Status = o.Status,
                        CustomerId = o.CustomerId,
                        TotalPrice = o.TotalPrice,
                        CurrencyCode = o.CurrencyCode,
                        ExchangeRate = o.ExchangeRate,
                        ForeignPrice = o.ForeignPrice,
                        IsClosed = o.IsClosed,
                        OrderDetails = o.OrderDetails.Select(od => new GetOrderDetailDto
                        {
                            OrderId = od.OrderId,
                            ItemId = od.ItemId,
                            ItemPrice = od.ItemPrice,
                            Quantity = od.Quantity,
                            TotalPrice = od.TotalPrice
                        }).ToList()
                    })
                    .ToList();

            return _responseHelper.RetrievedSuccessfully(orders, "Orders retrieved successfully");
        }
    }
}
