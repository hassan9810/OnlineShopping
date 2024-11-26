namespace Store.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize(Roles = "Customer, Admin")]
        [HttpGet("GetUserProfile")]
        public async Task<ResponseDto> GetLoggedInUserData() => await _mediator.Send(new GetLoggedInUserQuery());

        [Authorize(Roles = "Admin")]
        [HttpGet("GetAllUsers")]
        [EnableQuery]
        public async Task<ResponseDto> GetAllUsers() => await _mediator.Send(new GetAllUsersQuery());
    }
}
