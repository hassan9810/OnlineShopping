namespace Store.Features.Users.Query.GetAll
{
    public class GetAllUsersHandler : IRequestHandler<GetAllUsersQuery, ResponseDto>
    {
        private readonly UserManager<User> _userManager;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly ResponseHelper _response;
        private readonly IMapper _mapper;

        public GetAllUsersHandler(IHttpContextAccessor contextAccessor, ResponseHelper response, UserManager<User> userManager, IMapper mapper)
        {
            _contextAccessor = contextAccessor;
            _response = response;
            _userManager = userManager;
            _mapper = mapper;
        }
        public async Task<ResponseDto> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await _userManager.Users.ToListAsync();

            if (users == null || !users.Any())
                return _response.NotFound("No users found!");

            var usersData = _mapper.Map<List<UserDto>>(users);
            return _response.RetrievedSuccessfully(usersData, "Users Retrieved Successfully!");
        }
    }
}
