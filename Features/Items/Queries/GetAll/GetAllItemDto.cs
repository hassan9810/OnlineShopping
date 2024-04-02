namespace Store.Features.Items.Queries.GetAll
{
    public class GetAllItemDto
    {
        public int Id { get; set; }
        public string ItemName { get; set; }
        public string Description { get; set; }
        public int UomId { get; set; }
        public int Qty { get; set; }
        public decimal Price { get; set; }
    }
}
