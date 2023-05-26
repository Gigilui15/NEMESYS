using NEMESYS.Models;
namespace NEMESYS.ViewModels
{
    public class ReportPostViewModel
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string ImageUrl { get; set; }
        public int ReadCount { get; set; }
        public CategoryViewModel Category { get; set; }
        public ReporterViewModel Reporter { get; set; }
    }
}
