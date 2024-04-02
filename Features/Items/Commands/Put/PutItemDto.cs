namespace Store.Features.Items.Commands.Put
{
    public class PutItemDto
    {
        public string ItemName { get; set; }
        public string Description { get; set; }
        public int UomId { get; set; }
        public int Qty { get; set; }
        public decimal Price { get; set; }
    }
}
