namespace Store.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles ="Admin")]
    public class UOMController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UOMController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("AddNewUOM")]
        public async Task<ResponseDto> PostUOM(PostUOMsCommand command)
        {
            return await _mediator.Send(command);
        }

        [HttpDelete("DeleteUOM/{id}")]
        public async Task<ResponseDto> DeleteUOM(int id)
        {
            return await _mediator.Send(new DeleteUOMsCommand(id));
        }

        [HttpGet("GetAllUOMs")]
        [EnableQuery]
        public async Task<ResponseDto> GetAllUOMs()
        {
            return await _mediator.Send(new GetAllUOMsQuery());
        }

        [HttpPut("PutUOM/{id}")]
        public async Task<ResponseDto> PutUOM(int id, PutUOMsDto putUOMDto)
        {
            return await _mediator.Send(new PutUOMsCommand(id, putUOMDto));
        }
    }
}
