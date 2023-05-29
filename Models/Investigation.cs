namespace NEMESYS.Models
{
    public class Investigation
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}
