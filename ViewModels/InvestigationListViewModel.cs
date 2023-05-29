using NEMESYS.Models;

namespace NEMESYS.ViewModels
{
    public class InvestigationListViewModel
    {
        public int TotalEntries { get; set; }
        public IEnumerable<Investigation> Investigations { get; set; }
    }
}
