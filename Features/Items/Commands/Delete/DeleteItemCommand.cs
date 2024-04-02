namespace Store.Features.Items.Commands.Delete
{
    public class DeleteItemCommand : IRequest<ResponseDto>
    {
        public int ItemId { get; set; }
        public DeleteItemCommand(int itemId)
        {
            ItemId = itemId;
        }
    }
}
