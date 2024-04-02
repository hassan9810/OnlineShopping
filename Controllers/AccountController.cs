namespace Store.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AccountController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("Register")]
        public async Task<ResponseDto> Register(RegisterCommand command) => await _mediator.Send(command);

        [HttpPost("Login")]
        public async Task<ResponseDto> Login(LoginCommnad commnad) => await _mediator.Send(commnad);

    }
}
