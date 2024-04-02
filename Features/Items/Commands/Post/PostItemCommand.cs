namespace Store.Features.Items.Commands.Post
{
    public class PostItemCommand : IRequest<ResponseDto>
    {
        public string ItemName { get; set; }
        public string Description { get; set; }
        public int UomId { get; set; }
        public int Qty { get; set; }
        public decimal Price { get; set; }
    }
}
