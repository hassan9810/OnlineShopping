namespace Store.Features.Items.Commands.Put
{
    public class PutItemCommand : IRequest<ResponseDto>
    {
        public int ItemId { get; set; }
        public PutItemDto ItemDto { get; set; }

        public PutItemCommand(int itemId, PutItemDto itemDto)
        {
            ItemId = itemId;
            ItemDto = itemDto;
        }
    }
}
