using NEMESYS.Models;

namespace NEMESYS.ViewModels
{
    public class ReportPostListViewModel
    {
        public int TotalEntries { get; set; }
        public IEnumerable<ReportPostViewModel> ReportPosts { get; set; }
    }
}
