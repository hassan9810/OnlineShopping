namespace Store.Features.Account.Command.Register
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, ResponseDto>
    {
        private readonly UserManager<User> _userManager;

        private readonly IMapper _mapper;

        private readonly ResponseHelper _response;
        public RegisterCommandHandler(IMapper mapper, UserManager<User> userManager, ResponseHelper response)
        {

            _mapper = mapper;
            _userManager = userManager;
            _response = response;
        }

        public async Task<ResponseDto> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {

            var user = _mapper.Map<User>(request);

            var EmailExists = await _userManager.FindByEmailAsync(request.Email) is not null;
            if (EmailExists)
                return _response.AlreadyExists("Email Already Exists");

            var UserNameExists = await _userManager.FindByNameAsync(request.UserName) is not null;
            if (UserNameExists)
                return _response.AlreadyExists("UserName Already Exists");

            var result = await _userManager.CreateAsync(user, request.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "Customer");
                return _response.RetrievedSuccessfully(user.UserName, "User  Registered Successfully");
            }
            else
                return _response.FailedToSave("An Error Occurred Try Again Later");

        }
    }
}
