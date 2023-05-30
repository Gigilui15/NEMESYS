using System.ComponentModel.DataAnnotations;
using NEMESYS.Models;

namespace NEMESYS.ViewModels
{
    public class HallOfFameViewModel
    {
        public string Type { get; set; }
        public IEnumerable<HallOfFameEntry> HallOfFame { get; set; }

    }
}
