namespace Store.Features.UOMs.Queries.GetAll
{
    public class GetAllUOMsQueryHandler : IRequestHandler<GetAllUOMsQuery, ResponseDto>
    {
        private readonly IGRepository<UOM> _uomRepository;
        private readonly IMapper _mapper;
        private readonly ResponseHelper _response;

        public GetAllUOMsQueryHandler(IGRepository<UOM> uomRepository, IMapper mapper, ResponseHelper response)
        {
            _uomRepository = uomRepository;
            _mapper = mapper;
            _response = response;
        }

        public async Task<ResponseDto> Handle(GetAllUOMsQuery request, CancellationToken cancellationToken)
        {
            var allUOMs = _uomRepository.GetAll().ToList();

            if (!allUOMs.Any())
            {
                return _response.NotFound("UOMs not found");
            }

            var mappedUOMs = _mapper.Map<List<GetAllUOMsDto>>(allUOMs);

            return _response.RetrievedSuccessfully(mappedUOMs, "UOMs retrieved successfully");
        }
    }

}
