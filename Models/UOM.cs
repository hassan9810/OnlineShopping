namespace Store.Models
{
    public class UOM : BaseEntity
    {
        public string UnitMeasure { get; set; }
        public string Description { get; set; }

        public ICollection<Item> Items { get; set; }
    }
}