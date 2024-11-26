namespace Store.Models
{
    public class Item : BaseEntity
    {
        public string ItemName { get; set; }
        public string Description { get; set; }
        public int UomId { get; set; }
        public int Qty { get; set; }
        public decimal Price { get; set; }

        public ICollection<OrderDetail> OrderDetails { get; set; }
        public UOM Uom { get; set; }
    }
}