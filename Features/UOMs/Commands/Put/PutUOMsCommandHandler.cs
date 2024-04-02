namespace Store.Features.UOMs.Commands.Put
{
    public class PutUOMsCommandHandler : IRequestHandler<PutUOMsCommand, ResponseDto>
    {
        private readonly IGRepository<UOM> _uomRepository;
        private readonly IMapper _mapper;
        private readonly ResponseHelper _response;

        public PutUOMsCommandHandler
        (
            IGRepository<UOM> uomRepository,
            IMapper mapper,
            ResponseHelper response
        )
        {
            _uomRepository = uomRepository;
            _mapper = mapper;
            _response = response;
        }

        public async Task<ResponseDto> Handle(PutUOMsCommand request, CancellationToken cancellationToken)
        {
            var uom = _uomRepository.GetFirst(u => u.Id == request.UomId);
            if (uom == null)
            {
                return _response.NotFound("UOM not found");
            }

            var mappedUOM = _mapper.Map<PutUOMsDto, UOM>(request.UomDto, uom);

            _uomRepository.Update(mappedUOM);
            _uomRepository.Save();

            return _response.SavedSuccessfully("UOM updated successfully");
        }
    }
}
