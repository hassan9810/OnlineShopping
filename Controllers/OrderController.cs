namespace Store.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrderController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize(Roles = "Customer, Admin")]
        [HttpPost("AddNewOrder")]
        public async Task<ResponseDto> PostOrder(PostOrderCommand command) => await _mediator.Send(command);

        [Authorize(Roles = "Admin")]
        [HttpPut("ChangeOrderStatus/{orderId}")]
        public async Task<ResponseDto> PutOrder(int orderId, bool isClosed)
        {
            return await _mediator.Send(new PutOrderIsClosedCommand(orderId, isClosed));
        }

        [Authorize(Roles = "Customer, Admin")]
        [HttpGet("GetAllMyOrders")]
        public async Task<ResponseDto> GetAllMyOrders() => await _mediator.Send(new GetOrdersByLoggedInUserQuery());

        [Authorize(Roles = "Admin")]
        [HttpGet("GetOrderById/{id}")]
        public async Task<ResponseDto> GetOrderById(int id) => await _mediator.Send(new GetOrderByIdQuery(id));
    }
}
