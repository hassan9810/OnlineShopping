namespace Store.Features.Items.Queries.GetById
{
    public class GetItemByIdDto
    {
        public int Id { get; set; }
        public string ItemName { get; set; }
        public string Description { get; set; }
        public int UomId { get; set; }
        public int Qty { get; set; }
        public decimal Price { get; set; }
    }
}
