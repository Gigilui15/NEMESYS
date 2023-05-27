using System.ComponentModel.DataAnnotations;

namespace NEMESYS.ViewModels
{
    public class ReportDashboardViewModel
    {
        [Display(Name = "Total Reports")]
        public int TotalEntries { get; set; }
        [Display(Name = "Total Registered Users")]
        public int TotalRegisteredUsers { get; set; }


    }
}
