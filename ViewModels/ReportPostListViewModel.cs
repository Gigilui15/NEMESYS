using NEMESYS.Models;

namespace NEMESYS.ViewModels
{
    public class ReportPostListViewModel
    {
        public int TotalEntries { get; set; }
        public IEnumerable<Report> ReportPost { get; set; }

    }
}
