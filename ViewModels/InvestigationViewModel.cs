namespace NEMESYS.ViewModels
{
    public class InvestigationViewModel
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public UserViewModel User { get; set; }
    }
}
