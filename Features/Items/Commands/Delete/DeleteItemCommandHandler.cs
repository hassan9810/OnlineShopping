namespace Store.Features.Items.Commands.Delete
{
    public class DeleteItemCommandHandler : IRequestHandler<DeleteItemCommand, ResponseDto>
    {
        private readonly IGRepository<Item> _itemRepo;
        private readonly ResponseHelper _response;

        public DeleteItemCommandHandler(ResponseHelper response, IGRepository<Item> itemRepo)
        {
            _response = response;
            _itemRepo = itemRepo;
        }

        public async Task<ResponseDto> Handle(DeleteItemCommand request, CancellationToken cancellationToken)
        {
            var item = _itemRepo.GetFirst(a => a.Id == request.ItemId);
            if (item == null)
                return _response.NotFound("Item Not Found!");

            _itemRepo.Remove(item);
            _itemRepo.Save();

            return _response.SavedSuccessfully("Item Deleted Successfully");
        }
    }
}
