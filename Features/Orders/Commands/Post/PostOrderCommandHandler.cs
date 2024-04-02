namespace Store.Features.Orders.Post
{
    public class PostOrderCommandHandler : IRequestHandler<PostOrderCommand, ResponseDto>
    {
        private readonly IMapper _mapper;
        private readonly IGRepository<Order> _orderRepository;
        private readonly IGRepository<Item> _itemRepository;
        private readonly IGRepository<OrderDetail> _orderDetailsRepository;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IConfiguration _configuration;
        private readonly int loggedInUserId;
        private readonly ResponseHelper _response;

        public PostOrderCommandHandler
        (
            IMapper mapper,
            IGRepository<Order> orderRepository,
            IGRepository<OrderDetail> orderDetailsRepository,
            IGRepository<Item> itemRepository,
            IWebHostEnvironment hostEnvironment,
            IHttpContextAccessor contextAccessor,
            IConfiguration configuration,
            ResponseHelper response
        )
        {
            _mapper = mapper;
            _orderRepository = orderRepository;
            _orderDetailsRepository = orderDetailsRepository;
            _itemRepository = itemRepository;
            _configuration = configuration;
            _hostEnvironment = hostEnvironment;
            _contextAccessor = contextAccessor;
            _response = response;
            loggedInUserId = LoggedInUserProvider.GetLoggedInUserId(contextAccessor);
        }
        public async Task<ResponseDto> Handle(PostOrderCommand request, CancellationToken cancellationToken)
        {

            var validator = new PostOrderCommandValidator();

            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                string validationErrorMessage = string.Join(Environment.NewLine, validationResult.Errors.Select(e => e.ErrorMessage));
                return _response.FailedToSave(validationErrorMessage);
            }

            decimal totalPrice = CalculateOrderTotal(request.OrderDetails, request.DiscountPromoCode);

            string currencyCode = string.IsNullOrEmpty(request.CurrencyCode)
                                 ? _configuration["AppSettings:DefaultCurrency"]
                                 : request.CurrencyCode;

            string validationMessage = await ValidateItemQuantitiesCases(request.OrderDetails);
            if (!string.IsNullOrEmpty(validationMessage))
            {
                return _response.FailedToSave(validationMessage);
            }

            var newOrder = new Order
            {
                RequestDate = DateTime.UtcNow,
                CustomerId = loggedInUserId,
                TotalPrice = totalPrice,
                CurrencyCode = currencyCode,
                HaveVoucher = request.HaveVoucher,
                Status = OrderStatus.Open, 
                DiscountPromoCode = request.HaveVoucher ? request.DiscountPromoCode : null, 
                DiscountValue = GetDiscountValueFromConfig(request.DiscountPromoCode),                                                                  
                CloseDate = null,
                ExchangeRate = GetExchangeRate(request.CurrencyCode),
                ForeignPrice = ConvertForeignPrice(totalPrice, request.CurrencyCode),
                OrderDetails = await CreateOrderDetails(request.OrderDetails, totalPrice)
            };

            await DecreaseItemQuantities(request.OrderDetails);
            await _orderRepository.AddAsync(newOrder);
            _orderRepository.Save();

            return _response.SavedSuccessfully(newOrder.Id, "Order Created Successfully!");
        }
        private decimal CalculateOrderTotal(List<OrderDetailDto> orderDetails, string discountPromoCode)
        {
            decimal totalPrice = 0;

            foreach (var orderDetail in orderDetails)
            {
                var item = _itemRepository.GetFirst(item => item.Id == orderDetail.ItemId);
                if (item != null)
                {
                    totalPrice += item.Price * orderDetail.Quantity;
                }
            }

            if (!string.IsNullOrWhiteSpace(discountPromoCode))
            {
                decimal discountValue = GetDiscountValueFromConfig(discountPromoCode);
                totalPrice -= discountValue;
            }

            return totalPrice;
        }

        private decimal GetDiscountValueFromConfig(string promoCode)
        {
            var discountValue = _configuration["AppSettings:"+promoCode];
            if (decimal.TryParse(discountValue, out decimal result))
            {
                return result;
            }
            return 0;
        }
        private decimal ConvertForeignPrice(decimal totalPrice, string currencyCode)
        {
             if (currencyCode == "USD")
                {
                    return totalPrice / 30M;
                }
                else if (currencyCode == "EUR")
                {
                    return totalPrice / 32M;
                }
                else
                {
                    return totalPrice;
                }
        }
        public decimal GetExchangeRate(string currencyCode)
        {
            if (currencyCode == "USD")
            {
                return 30M;
            }
            else if (currencyCode == "EUR")
            {
                return 32M;
            }
            else
            {
                return 1.0M;
            }
        }
        private async Task DecreaseItemQuantities(List<OrderDetailDto> orderDetails)
        {
            foreach (var orderDetail in orderDetails)
            {
                var item = _itemRepository.GetFirst(item => item.Id == orderDetail.ItemId);
                if (item != null)
                {
                    item.Qty -= orderDetail.Quantity;
                     _itemRepository.Update(item);
                }
            }
        }

        private async Task<bool> ValidateItemQuantities(List<OrderDetailDto> orderDetails)
        {
            foreach (var orderDetail in orderDetails)
            {
                var item = await _itemRepository.GetFirstAsync(item => item.Id == orderDetail.ItemId);
                if (item != null && item.Qty < orderDetail.Quantity)
                {
                    return false;
                }
            }
            return true;
        }

        private async Task<string> ValidateItemQuantitiesCases(List<OrderDetailDto> orderDetails)
        {
            foreach (var orderDetail in orderDetails)
            {
                var item = await _itemRepository.GetFirstAsync(item => item.Id == orderDetail.ItemId);
                if (item != null)
                {
                    if (item.Qty <= 0)
                    {
                        return $"Item '{item.ItemName}' is out of stock.";
                    }
                    else if (item.Qty < orderDetail.Quantity)
                    {
                        return $"Insufficient quantity for item '{item.ItemName}'. Available quantity: {item.Qty}.";
                    }
                }
                else
                {
                    return $"Item with ID '{orderDetail.ItemId}' not found.";
                }
            }
            return null;
        }
        private async Task SaveOrderDetails(int orderId, List<OrderDetailDto> orderDetails)
        {
            decimal orderTotalPrice = CalculateOrderTotal(orderDetails, null);

            foreach (var orderDetailDto in orderDetails)
            {
                var itemPrice = GetItemPrice(orderDetailDto.ItemId);

                var orderDetail = new OrderDetail
                {
                    OrderId = orderId,
                    ItemId = orderDetailDto.ItemId,
                    ItemPrice = itemPrice,
                    Quantity = orderDetailDto.Quantity,
                    TotalPrice = itemPrice * orderDetailDto.Quantity
                };

                await _orderDetailsRepository.AddAsync(orderDetail);
            }

            _orderDetailsRepository.Save();
        }
        private async Task<List<OrderDetail>> CreateOrderDetails(List<OrderDetailDto> orderDetails, decimal orderTotalPrice)
        {
            List<OrderDetail> orderDetailsList = new List<OrderDetail>();

            foreach (var orderDetailDto in orderDetails)
            {
                var itemPrice = GetItemPrice(orderDetailDto.ItemId);

                var orderDetail = new OrderDetail
                {
                    ItemId = orderDetailDto.ItemId,
                    ItemPrice = itemPrice,
                    Quantity = orderDetailDto.Quantity,
                    TotalPrice = orderTotalPrice * orderDetailDto.Quantity / orderDetails.Sum(od => od.Quantity),
                };

                orderDetailsList.Add(orderDetail);
            }
            return orderDetailsList;
        }
        private decimal GetItemPrice(int itemId)
        {
            var item = _itemRepository.GetFirst(i => i.Id == itemId);

            if (item != null)
            {
                return item.Price;
            }

            return 0;
        }

        private decimal CalculateOrderDetailTotal(int itemId, int quantity)
        {
            decimal itemPrice = GetItemPrice(itemId);
            decimal totalPrice = itemPrice * quantity;

            return totalPrice;
        }
    }
}

