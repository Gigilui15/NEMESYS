using Microsoft.AspNetCore.Identity;

namespace NEMESYS.Models
{
    public class ApplicationUser : IdentityUser
    {
        [PersonalData]
        public string ReporterAlias { get; set; }
    }
}
