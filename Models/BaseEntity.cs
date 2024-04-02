namespace Store.Models
{
    public class BaseEntity
    {
        public int Id { get; set; }
        [ForeignKey("User")]
        public int CreatedBy { set; get; }
        public User User { get; set; }
        public DateTime CreatedOn { set; get; }
        public int? UpdatedBy { set; get; }
        public DateTime? UpdatedOn { set; get; }
    }
}
