namespace Store.Features.Users.Query.GetLoggedInUser
{
    public class GetLoggedInUserQueryHandler : IRequestHandler<GetLoggedInUserQuery, ResponseDto>
    {
        private readonly UserManager<User> _userManager;
        private readonly int loggedInUserId;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly ResponseHelper _response;
        private readonly IMapper _mapper;

        public GetLoggedInUserQueryHandler(IHttpContextAccessor contextAccessor, ResponseHelper response, UserManager<User> userManager, IMapper mapper)
        {
            _contextAccessor = contextAccessor;
            _response = response;
            loggedInUserId = LoggedInUserProvider.GetLoggedInUserId(_contextAccessor);
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<ResponseDto> Handle(GetLoggedInUserQuery request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(loggedInUserId.ToString());
            if (user == null)
                _response.NotFound("User Not Found!");

            var userData = _mapper.Map<UserDto>(user);
            return _response.RetrievedSuccessfully(userData, "User Data Retrieved Successfully!");

        }
    }
}
