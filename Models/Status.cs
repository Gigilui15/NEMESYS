namespace NEMESYS.Models
{
    public class Status
    {
        public int Id { get; set; }
        public string Title { get; set; }

        //Collection navigation property
        public List<Report> Reports { get; set; }
    }
}
