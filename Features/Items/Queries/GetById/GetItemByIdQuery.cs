namespace Store.Features.Items.Queries.GetById
{
    public class GetItemByIdQuery : IRequest<ResponseDto>
    {
        public int ItemId { get; set; }

        public GetItemByIdQuery(int itemId)
        {
            ItemId = itemId;
        }
    }
}
