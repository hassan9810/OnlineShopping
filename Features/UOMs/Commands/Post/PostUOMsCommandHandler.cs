namespace Store.Features.UOMs.Commands.Post
{
    public class PostUOMsCommandHandler : IRequestHandler<PostUOMsCommand, ResponseDto>
    {
        private readonly IMapper _mapper;
        private readonly IGRepository<UOM> _uomRepository;
        private readonly ResponseHelper _response;

        public PostUOMsCommandHandler
        (
            IMapper mapper,
            IGRepository<UOM> uomRepository,
            ResponseHelper response
        )
        {
            _mapper = mapper;
            _uomRepository = uomRepository;
            _response = response;
        }
        public async Task<ResponseDto> Handle(PostUOMsCommand request, CancellationToken cancellationToken)
        {
            var newUOM = _mapper.Map<UOM>(request);

            await _uomRepository.AddAsync(newUOM);
            _uomRepository.Save();

            return _response.SavedSuccessfully(newUOM.Id, "UOM Created Successfully!");
        }
    }
}
