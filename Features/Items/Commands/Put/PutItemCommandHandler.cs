namespace Store.Features.Items.Commands.Put
{
    public class PutItemCommandHandler : IRequestHandler<PutItemCommand, ResponseDto>
    {
        private readonly IGRepository<Item> _itemRepository;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly int loggedInUserId;
        private readonly ResponseHelper _response;
        private readonly IMapper _mapper;

        public PutItemCommandHandler(IGRepository<Item> itemRepository, IWebHostEnvironment hostEnvironment, IHttpContextAccessor contextAccessor, ResponseHelper response, IMapper mapper)
        {
            _itemRepository = itemRepository;
            _hostEnvironment = hostEnvironment;
            _contextAccessor = contextAccessor;
            _response = response;
            loggedInUserId = LoggedInUserProvider.GetLoggedInUserId(contextAccessor);
            _mapper = mapper;
        }
        public async Task<ResponseDto> Handle(PutItemCommand request, CancellationToken cancellationToken)
        {
            var item = _itemRepository.GetFirst(i => i.Id == request.ItemId);
            if (item == null)
                return _response.NotFound("Item not found");

            var mappedItem = _mapper.Map<PutItemDto, Item>(request.ItemDto, item);

            mappedItem.CreatedBy = item.CreatedBy;
            mappedItem.CreatedOn = item.CreatedOn;
            mappedItem.UpdatedBy = loggedInUserId;

            _itemRepository.Update(mappedItem);
            _itemRepository.Save();

            return _response.SavedSuccessfully("Item updated successfully");
        }
    }
}
