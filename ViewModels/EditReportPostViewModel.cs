using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Xml.Linq;

namespace NEMESYS.ViewModels
{
    public class EditReportPostViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "A title is required")]
        [StringLength(50)]
        [Display(Name = "Report heading")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Report description is required")]
        [StringLength(1500, ErrorMessage = "The Report description cannot be longer than 1500 characters")]
        public string Content { get; set; }

        //Nullable (will be ignored by the model binder and model state validation - non-nullable == required)
        public string? ImageUrl { get; set; }

        [Display(Name = "Featured Image")]
        public IFormFile? ImageToUpload { get; set; } //used only when submitting form

        [Display(Name = "Report Post Category")]
        //Property used to bind user selection.
        [Required(ErrorMessage = "Report Post Category is required")]
        public int CategoryId { get; set; }

        //Property used solely to populate drop down
        public List<CategoryViewModel>? CategoryList { get; set; }
        public List<StatusViewModel>? StatusList { get; set; }

    }
}
