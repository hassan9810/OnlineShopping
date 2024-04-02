namespace Store.Features.Items.Queries.GetById
{
    public class GetItemByIdQueryHandler : IRequestHandler<GetItemByIdQuery, ResponseDto>
    {
        private readonly IGRepository<Item> _itemRepo;
        private readonly ResponseHelper _responseHelper;
        private readonly IMapper _mapper;

        public GetItemByIdQueryHandler(IGRepository<Item> itemRepo, IMapper mapper)
        {
            _itemRepo = itemRepo;
            _responseHelper = new ResponseHelper();
            _mapper = mapper;
        }

        public async Task<ResponseDto> Handle(GetItemByIdQuery request, CancellationToken cancellationToken)
        {
            var item = await _itemRepo.GetFirstAsync(i => i.Id == request.ItemId);

            if (item == null)
            {
                return _responseHelper.NotFound("Item not found");
            }

            var itemDto = _mapper.Map<GetItemByIdDto>(item);
            return _responseHelper.RetrievedSuccessfully(itemDto, "Item retrieved successfully");
        }
    }
}
