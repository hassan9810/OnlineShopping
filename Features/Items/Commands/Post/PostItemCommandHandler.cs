namespace Store.Features.Items.Commands.Post
{
    public class PostItemCommandHandler : IRequestHandler<PostItemCommand, ResponseDto>
    {
        private readonly IMapper _mapper;
        private readonly IGRepository<Item> _itemRepository;
        private readonly ResponseHelper _response;

        public PostItemCommandHandler
        (
            IMapper mapper,
            IGRepository<Item> itemRepository,
            ResponseHelper response
        )
        {
            _mapper = mapper;
            _itemRepository = itemRepository;
            _response = response;
        }
        public async Task<ResponseDto> Handle(PostItemCommand request, CancellationToken cancellationToken)
        {
            var newItem = _mapper.Map<Item>(request);

            await _itemRepository.AddAsync(newItem);
            _itemRepository.Save();

            return _response.SavedSuccessfully(newItem.Id, "Item Created Successfully!");
        }
    }
}
