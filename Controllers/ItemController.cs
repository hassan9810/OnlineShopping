namespace Store.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles ="Admin")]
    public class ItemController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ItemController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("PostItem")]
        public async Task<ResponseDto> PostItem(PostItemCommand command) => await _mediator.Send(command);

        [HttpDelete("DeleteItem/{id}")]
        public async Task<ResponseDto> DeleteItem(int id) => await _mediator.Send(new DeleteItemCommand(id));

        [HttpGet("GetAllItems")]
        [EnableQuery]
        public async Task<ResponseDto> GetAllItems() => await _mediator.Send(new GetAllItemQuery());

        [HttpGet("GetItemById/{id}")]
        public async Task<ResponseDto> GetItemById(int id) => await _mediator.Send(new GetItemByIdQuery(id));
        [HttpPut("PutItem/{id}")]
        public async Task<ResponseDto> PutItem(int id, PutItemDto putItemDto)
        {
            return await _mediator.Send(new PutItemCommand(id, putItemDto));
        }
    }
}
