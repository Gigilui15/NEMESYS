using System.ComponentModel.DataAnnotations;

namespace NEMESYS.ViewModels
{
    public class EditInvestigationViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Investigation description is required")]
        [StringLength(1500, ErrorMessage = "Investigation cannot be longer than 1500 characters")]
        public string Content { get; set; }
    }
}
