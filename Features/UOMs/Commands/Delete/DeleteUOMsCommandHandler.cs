namespace Store.Features.UOMs.Commands.Delete
{
    public class DeleteUOMsCommandHandler : IRequestHandler<DeleteUOMsCommand, ResponseDto>
    {
        private readonly IGRepository<UOM> _uomRepository;
        private readonly ResponseHelper _response;

        public DeleteUOMsCommandHandler(IGRepository<UOM> uomRepository, ResponseHelper response)
        {
            _uomRepository = uomRepository;
            _response = response;
        }

        public async Task<ResponseDto> Handle(DeleteUOMsCommand request, CancellationToken cancellationToken)
        {
            var uom = _uomRepository.GetFirst(u => u.Id == request.UomId);
            if (uom == null)
            {
                return _response.NotFound("UOM not found");
            }

            _uomRepository.Remove(uom);
            _uomRepository.Save();

            return _response.SavedSuccessfully("UOM deleted successfully");
        }
    }
}
