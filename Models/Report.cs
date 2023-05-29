using NEMESYS.Models;

namespace NEMESYS.Models
{
    public class Report
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string ImageUrl { get; set; }

        //Foreign Key - navigation property (name + key as the property name)
        public int CategoryId { get; set; }
        //Reference navigation property
        public Category Category { get; set; }

        //Foreign Key - navigation property name + key property name
        public string UserId { get; set; }
        //Reference navigation property
        public ApplicationUser User { get; set; }
        public int? InvestigationId { get; set; }
        public Investigation? Investigation { get; set; }
        public int StatusId { get; set; }
    }

}
