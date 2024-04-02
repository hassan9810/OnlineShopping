namespace Store.Features.Items.Queries.GetAll
{
    public class GetAllItemQueryHandler : IRequestHandler<GetAllItemQuery, ResponseDto>
    {
        private readonly IGRepository<Item> _itemRepo;
        private readonly int loggedInUserId;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly ResponseHelper _response;
        private readonly IMapper _mapper;

        public GetAllItemQueryHandler(IGRepository<Item> itemRepo,
                                      IHttpContextAccessor contextAccessor,
                                      ResponseHelper response,
                                      IMapper mapper)
        {
            _itemRepo = itemRepo;
            _contextAccessor = contextAccessor;
            loggedInUserId = LoggedInUserProvider.GetLoggedInUserId(_contextAccessor);
            _response = response;
            _mapper = mapper;
        }

        public async Task<ResponseDto> Handle(GetAllItemQuery request, CancellationToken cancellationToken)
        {
            var allItems = _itemRepo.GetAll().ToList();

            if (!allItems.Any())
            {
                return _response.NotFound("ItemsNotFound!");
            }

            var mappedItems = _mapper.Map<List<GetAllItemDto>>(allItems);


            return _response.RetrievedSuccessfully(mappedItems, "Items Retrieved Successfully");
        }
    }
}
